using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using image_processing.Utilities;
namespace ImageProcessingTests
{
    [TestClass]
    public class UnitTest1
    {
       

        [TestMethod]
        public void Bmp_Values_Are_Equals_to_Array2D_values()
        {
            
            Bitmap bitmap = new Bitmap(10, 10, PixelFormat.Format8bppIndexed);

           
            var data = bitmap.LockBits(new Rectangle(0, 0, 10, 10), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            int bytes = Math.Abs(data.Stride) * data.Height;

            Byte[] values = new byte[bytes];


            Random r = new Random(DateTime.Now.Millisecond);
            r.NextBytes(values);
            Marshal.Copy(values, 0, data.Scan0, bytes);

            bitmap.UnlockBits(data);

            BlobMomentum blobMomentum = new BlobMomentum(bitmap);

            Assert.IsTrue(Check(blobMomentum));
            
        }

        private bool Check(BlobMomentum blobMomentum)
        {
            bool result = true;
            var bmp = blobMomentum.Bmp;
            var array = blobMomentum.Array2D;
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                    if (bmp.GetPixel(i, j).ToString() != Color.FromArgb((array[i, j])).ToString())
                    {
                        result = false;
                        break;
                    }

            return result;
        }
    }
}
