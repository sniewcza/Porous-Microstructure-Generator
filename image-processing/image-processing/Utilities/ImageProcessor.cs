using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Generator.Model;

namespace Generator.Utilities
{
    class ImageProcessor : IImageProcessor
    {
       
        public event EventHandler OnProgress;
        public event EventHandler<int> OnStart;
        public event EventHandler OnEnd;

        public Bitmap Binarization(Bitmap bitmap, int threshold)
        {
            Threshold filter = new Threshold(threshold);
            return filter.Apply(bitmap);
        }

        public Bitmap Closing(Bitmap bitmap)
        {
            Closing filter = new Closing();
            return filter.Apply(bitmap);
        }

        public Bitmap Dilatation(Bitmap bitmap)
        {
            Dilatation filter = new Dilatation();
            return filter.Apply(bitmap);
        }

        public Bitmap Erosion(Bitmap bitmap)
        {
            Erosion filter = new Erosion();
            return filter.Apply(bitmap);
        }

        public List<PoreAnalyzeData> FindShapes(Bitmap bitmap)
        {
            Bitmap reversedbmp = ReverseBitmapColors(bitmap);
            BlobCounter blobCounter = new BlobCounter(reversedbmp);
            Blob[] blobs = blobCounter.GetObjects(reversedbmp, false);

            PoreAnalyzeData[] poreData = new PoreAnalyzeData[blobs.Length];
            OnStart?.Invoke(this, blobs.Length);

            Parallel.For(0, blobs.Length, index =>
            {
                var edgePoints = blobCounter.GetBlobsEdgePoints(blobs[index]);
                poreData[index] = new PoreAnalyzeData(blobs[index], ReverseBitmapColors(blobs[index].Image.ToManagedImage()), edgePoints);
                OnProgress?.Invoke(this, new EventArgs());
            });

            return poreData.ToList();
        }

        public Bitmap Opening(Bitmap bitmap)
        {
            Opening filter = new Opening();
            return filter.Apply(bitmap);
        }

        public Bitmap ReverseBitmapColors(Bitmap bitmap)
        {
            var newbmp = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
            var bitmapdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            var ptr = bitmapdata.Scan0;
            int bytes = Math.Abs(bitmapdata.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            bitmap.UnlockBits(bitmapdata);

            int max = rgbValues.Max();
            Parallel.For(0, rgbValues.Length, index =>
             {
                 rgbValues[index] = rgbValues[index] == max ? Byte.Parse("0") : Byte.Parse("255");

             });
            bitmapdata = newbmp.LockBits(new Rectangle(0, 0, newbmp.Width, newbmp.Height), ImageLockMode.ReadWrite, newbmp.PixelFormat);
            ptr = bitmapdata.Scan0;
            Marshal.Copy(rgbValues, 0, ptr, bytes);
            newbmp.UnlockBits(bitmapdata);

            return newbmp;
        }
      
        public Bitmap Skeletonization(Bitmap bitmap)
        {
            SimpleSkeletonization filter = new SimpleSkeletonization(255, 0);
            return filter.Apply(bitmap);
        }

        public double GetPoresVolume(Bitmap bitmap)
        {
            var bitmapdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            var ptr = bitmapdata.Scan0;
            int bytes = Math.Abs(bitmapdata.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            bitmap.UnlockBits(bitmapdata);

            int total = rgbValues.Where(b => b == 0).Count();
            return Math.Round((total / (double)rgbValues.Length) * 100, 2);
        }

        public List<int> BlobsArea(Bitmap bitmap)
        {
            Bitmap reversedBmp = ReverseBitmapColors(bitmap);
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage(reversedBmp);
            var blobs = blobCounter.GetObjectsInformation();

            int[] blobsvec = new int[blobs.Length];
            Parallel.For(0, blobs.Length, (index) =>
              {
                  blobsvec[index] = blobs[index].Area;
              });

            List<int> BlobsArea = blobsvec.ToList();
            BlobsArea.Sort();

            return BlobsArea;
        }

        public Bitmap ConvertToGrayscale(Bitmap original)
        {
            if (original.PixelFormat != PixelFormat.Format8bppIndexed && original.PixelFormat != PixelFormat.Format16bppGrayScale)
            {
                return Grayscale.CommonAlgorithms.RMY.Apply(original);
            }
            else
            {
                return original;
            }
        }

        public Bitmap FilterBloobs(Bitmap bitmap, int minWidth, int minHeight)
        {
            BlobsFiltering filter = new BlobsFiltering(minWidth, minHeight, int.MaxValue, int.MaxValue);           
            return ReverseBitmapColors(filter.Apply(ReverseBitmapColors(bitmap)));
        }
    }
}
