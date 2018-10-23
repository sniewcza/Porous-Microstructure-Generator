﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AForge.Imaging;
using AForge.Imaging.Filters;


namespace image_processing.Utilities
{
    class ImageProcessor : IImageProcessor
    {
        private BlobCounter _blobCounter;

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

        public List<PoreData> FindShapes(Bitmap bitmap)
        {
            var reversedbmp = ReverseBitmapColors(bitmap);
            _blobCounter = new BlobCounter(reversedbmp);
            var blobs = _blobCounter.GetObjects(reversedbmp, false);
            PoreData[] poreData = new PoreData[blobs.Length];

            OnStart(this, blobs.Length);
            // var bitmapdata = bmp.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            //SimpleShapeChecker shapeChecker = new SimpleShapeChecker();
            // var g = Graphics.FromImage(bmp);

            //shapeChecker.MinAcceptableDistortion = 0.5f;
            //shapeChecker.RelativeDistortionLimit = 0.07f;

            //foreach (var blob in _blobs)
            //{

            //    var edgePoints = _blobCounter.GetBlobsEdgePoints(blob);
            //    var points = edgePoints.Select(p => new Point(p.X, p.Y)).ToArray();
            //    if(edgePoints.Count !=1 )
            //    //switch(shapeChecker.CheckShapeType(edgePoints))
            //    //{
            //    //    case ShapeType.Circle:
            //    //        foreach (Point p in points)                       
            //    //            g.DrawEllipse(new Pen(Color.Red), p.X - 1, p.Y - 1, 0.5f, 0.5f);                       
            //    //        break;
            //    //    case ShapeType.Quadrilateral:
            //    //        foreach (Point p in points)
            //    //            g.DrawEllipse(new Pen(Color.Orange), p.X - 1, p.Y - 1, 0.5f, 0.5f);
            //    //        break;
            //    //    case ShapeType.Triangle:
            //    //        foreach (Point p in points)
            //    //            g.DrawEllipse(new Pen(Color.Purple), p.X - 1, p.Y - 1, 0.5f, 0.5f);
            //    //        break;
            //    //    case ShapeType.Unknown:
            //    //        foreach (Point p in points)
            //    //            g.DrawEllipse(new Pen(Color.Brown), p.X - 1, p.Y - 1, 0.5f, 0.5f);
            //    //        break;
            //    //}


            //    BlobsMomemntum.Add(new BlobMomentum(blob,ReverseBitmapColors( blob.Image.ToManagedImage()), edgePoints));


            //}

            Parallel.For(0, blobs.Length, index =>
            {
                var edgePoints = _blobCounter.GetBlobsEdgePoints(blobs[index]);
                poreData[index] = new PoreData(blobs[index], ReverseBitmapColors(blobs[index].Image.ToManagedImage()), edgePoints);
                OnProgress(this, new EventArgs());
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

        public Bitmap CreateUnindexedBitmap(Bitmap original)
        {
            var unindexedbmp = new Bitmap(original.Width, original.Height);
            var originalbmpData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, original.PixelFormat);
            var unindexedbmpData = unindexedbmp.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadWrite, original.PixelFormat);
            var originalbmpptr = originalbmpData.Scan0;

            var unindexedbmpptr = unindexedbmpData.Scan0;

            int bytes = Math.Abs(originalbmpData.Stride) * original.Height;

            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(originalbmpptr, rgbValues, 0, bytes);

            Marshal.Copy(rgbValues, 0, unindexedbmpptr, bytes);

            original.UnlockBits(originalbmpData);

            unindexedbmp.UnlockBits(unindexedbmpData);

            return unindexedbmp;
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

            BlobCounter blobCounter = new BlobCounter(reversedBmp);

            var blobs = blobCounter.GetObjects(reversedBmp, false);

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
