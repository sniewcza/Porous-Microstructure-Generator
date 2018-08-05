using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
namespace image_processing.Utilities
{
    public class BlobMomentum
    {
        private Bitmap _bmp;
        private int[,] _array2D;
        private int _centralX;
        private int _centralY;
        public double M1 { get; set; }
        public double M2 { get; set; }
        public double M3 { get; set; }
        public double M4 { get; set; }
        public double M5 { get; set; }
        public double M6 { get; set; }
        public double M7 { get; set; }
        public int CentralX { get => _centralX; set => _centralX = value; }
        public int CentralY { get => _centralY; set => _centralY = value; }
        public Bitmap Bmp { get => _bmp; set => _bmp = value; }
        public int[,] Array2D { get => _array2D; set => _array2D = value; }

        public BlobMomentum(Bitmap bitmap)
        {
            Bmp = bitmap;
            Generate2DArrayFromBmp();
            CentralX = Convert.ToInt32(getMomentum(1, 0) / getMomentum(0, 0));
            CentralY = Convert.ToInt32(getMomentum(0, 1) / getMomentum(0, 0));

          double  m00 = getMomentum(0, 0);

            M1 = (getCentralMomentum(2, 0) + getCentralMomentum(0, 2)) / Math.Pow(m00, 2);

            M2 = (Math.Pow(getCentralMomentum(2, 0) + getCentralMomentum(0, 2), 2) + 4 * Math.Pow(getCentralMomentum(1, 1), 2)) / Math.Pow(m00, 4);

            M3 = (Math.Pow(getCentralMomentum(3, 0) + 3 * getCentralMomentum(1, 2), 2) + Math.Pow(3 * getCentralMomentum(2, 1) - getCentralMomentum(0, 3), 2)) / Math.Pow(m00, 5);

            M4 = (Math.Pow(getCentralMomentum(3, 0) + getCentralMomentum(1, 2), 2) + Math.Pow(getCentralMomentum(2, 1) - getCentralMomentum(0, 3), 2)) / Math.Pow(m00, 5);

            M5 = ((getCentralMomentum(3, 0) - 3 * getCentralMomentum(1, 2)) * (getCentralMomentum(3, 0) + getCentralMomentum(1, 2)) * (Math.Pow(getCentralMomentum(3, 0) + getCentralMomentum(1, 2), 2) - 3 * (Math.Pow(getCentralMomentum(2, 1) + getCentralMomentum(0, 3), 2))) +

                (3 * getCentralMomentum(2, 1) - getCentralMomentum(0, 3)) * (getCentralMomentum(2, 1) + getCentralMomentum(0, 3)) * (3 * Math.Pow(getCentralMomentum(3, 0) + getCentralMomentum(1, 2), 2) - Math.Pow(getCentralMomentum(2, 1) + getCentralMomentum(0, 3), 2))) / Math.Pow(m00, 10);

            M6 = ((getCentralMomentum(2, 0) - getCentralMomentum(0, 2)) * (Math.Pow(getCentralMomentum(3, 0) + getCentralMomentum(1, 2), 2) - Math.Pow(getCentralMomentum(2, 1) + getCentralMomentum(0, 3), 2)) +

                4 * getCentralMomentum(1, 1) * (getCentralMomentum(3, 0) + getCentralMomentum(1, 2)) * (getCentralMomentum(2, 1) + getCentralMomentum(0, 3))) / Math.Pow(m00, 7);

            M7 = (getCentralMomentum(2, 0) * getCentralMomentum(0, 2) - Math.Pow(getCentralMomentum(1, 1), 2)) / Math.Pow(m00, 4); 
        }

        private double getMomentum(int Ptier, int Qtier)
        {

            //double m00 = 0;
            //for (int i = 0; i < Bmp.Width; i++)
            //    for (int j = 0; j < Bmp.Height; j++)
            //        if (Array2D[i, j] == 0)
            //            m00 += 1 * Math.Pow(i, Ptier) * Math.Pow(j, Qtier);

            double momentum = 0;
            for (int i = 0; i < _bmp.Width; i++)
                for (int j = 0; j < _bmp.Height; j++)
                    momentum += (_bmp.GetPixel(i, j).ToArgb() == Color.Black.ToArgb() ? 1 : 0) * Math.Pow(i, Ptier) * Math.Pow(j, Qtier);



            //Parallel.For(0, _bmp.Height, () => 0, (i, state, partial) =>
            //    {
            //        for (int j = 0; j < width; j++)
            //        {
            //            int tmp = _array2D[i, j];
            //            if (tmp != 0)
            //            {
            //                partial += (i^Ptier) * (j^Qtier) * tmp;
            //            }
            //        }
            //        return partial;
            //    },
            // (partial) => total += partial);


            return momentum;
        }

        private void Generate2DArrayFromBmp()
        {
            var bmpData = Bmp.LockBits(new Rectangle(0, 0, Bmp.Width, Bmp.Height), ImageLockMode.ReadOnly, Bmp.PixelFormat);

            Array2D = new int[Bmp.Width, Bmp.Height];
            int bytes = Math.Abs(bmpData.Stride) * bmpData.Height;
            byte[] rgbValues = new byte[bytes];
            IntPtr ptr = bmpData.Scan0;
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            Bmp.UnlockBits(bmpData);
            for (int x = 0; x < Bmp.Width; x++)
            {
                for (int y = 0; y < Bmp.Height; y++)
                {
                    int m = x + y * bmpData.Stride;
                    Array2D[x, y] = rgbValues[m];

                }
            }
        }

        private double getCentralMomentum(int Ptier, int Qtier)
        {
            double momentum = 0;
            for (int i = 0; i < _bmp.Width; i++)
                for (int j = 0; j < _bmp.Height; j++)
                    momentum += (_bmp.GetPixel(i, j).ToArgb() == Color.Black.ToArgb() ? 1 : 0) * Math.Pow(i-CentralX, Ptier) * Math.Pow(j-CentralY, Qtier);

            return momentum;
        }

    }
}
