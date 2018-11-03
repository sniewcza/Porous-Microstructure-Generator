using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using Generator.Model;

namespace Generator.Utilities
{
    public class MicrostructureGenerator
    {
        public event EventHandler<double> OnProgress;
        private Bitmap _bmp;
        private Random _random;
        private int _numberOfTrials = 5;
        public MicrostructureGenerator(int width, int height)
        {
            _bmp = CreateWhiteBmp(width, height);
            _random = new Random();
        }

        private Bitmap CreateWhiteBmp(int width, int height)
        {
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);
            var ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bmpData.Height;
            byte[] rgbValues = new byte[bytes].Select(val => (byte)255).ToArray();
            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);

            return bmp;

        }

        public Bitmap GenerateMicrostructure(List<PoreDto> poresData, double volume)
        {

            var validPores = poresData.Where(p => p.PoreImage.Width < _bmp.Width && p.PoreImage.Height < _bmp.Height).ToList();
            double totalArea = _bmp.Width * _bmp.Height;
            double poreAreaPercentage;
            double coveredArea = 0;

            while (coveredArea < volume)
            {
                validPores = validPores.Where(p => (p.Area / totalArea * 100) < volume - coveredArea).ToList();
                PoreDto pore;
                if (validPores.Count == 0)
                {
                    pore = poresData.OrderBy(p => p.Area).First();
                }
                else
                {
                    var index = _random.Next(0, validPores.Count);
                    pore = validPores[index];
                }
                if (GenerateBlobs(pore, _numberOfTrials))
                {
                    poreAreaPercentage = (pore.Area / totalArea * 100);
                    coveredArea += poreAreaPercentage;
                    OnProgress(this, poreAreaPercentage);
                }
            }

            return _bmp;
        }

        private bool GenerateBlobs(PoreDto pore, int numberOfTrials)
        {
            pore.PoreImage.RotateFlip((RotateFlipType)_random.Next(0, 8));
            var poreImage = pore.PoreImage;
            Rectangle rectangle;
            Point rectangleCenter;
            for (int i = 0; i < numberOfTrials; i++)
            {
                int x = _random.Next((poreImage.Width / 2), _bmp.Width - poreImage.Width / 2);
                int y = _random.Next((poreImage.Height / 2), _bmp.Height - poreImage.Height / 2);
                rectangleCenter = new Point(x, y);
                rectangle = new Rectangle(new Point(rectangleCenter.X - poreImage.Width / 2, rectangleCenter.Y - poreImage.Height / 2), new Size(poreImage.Width, poreImage.Height));
                if (CheckRectangleIsClear(rectangle))
                {
                    DrawImage(rectangle, pore.PoreImage);
                    return true;
                }
            }
            return false;
        }

        private bool CheckRectangleIsClear(Rectangle rec)
        {
            for (int i = rec.Location.X; i < rec.Right; i++)
                for (int j = rec.Location.Y; j < rec.Bottom; j++)
                    if (_bmp.GetPixel(i, j).ToArgb() == Color.Black.ToArgb())
                        return false;
            return true;
        }

        private void DrawImage(Rectangle rectangle, Bitmap image)
        {
            using (var g = Graphics.FromImage(_bmp))
            {              
                g.DrawImage(image, rectangle.Location);
            }
        }
    }
}
