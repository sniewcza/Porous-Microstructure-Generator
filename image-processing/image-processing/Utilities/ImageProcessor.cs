using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging.Filters;
namespace image_processing.Utilities
{
    class ImageProcessor : IImageProcessor
    {
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
            Parallel.For(0, rgbValues.Length, index =>
             {
                 rgbValues[index] = rgbValues[index] == 255 ? Byte.Parse("0") : Byte.Parse("255");

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
    }
}
