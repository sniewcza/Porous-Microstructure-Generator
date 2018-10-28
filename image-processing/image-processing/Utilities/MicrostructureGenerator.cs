using image_processing.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace image_processing.Utilities
{
    public class MicrostructureGenerator
    {
        public event EventHandler<double> OnProgress;
        private Bitmap _bmp;
        private Random _random;
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
            //var ordered = poresData.OrderByDescending(bm => bm.Blob.Area).GroupBy(bm => bm.ShapeId);
            //foreach (var bm in ordered)
            //{
            //    GenerateBlobs(poresData.First(e => e.ShapeId == bm.Key), bm.Count());
            //    OnProgress(this, new EventArgs());
            //}

            //List<PoreData> clone = new List<PoreData>(poresData);
            var validPores = poresData.Where(p => p.PoreImage.Width < _bmp.Width && p.PoreImage.Height < _bmp.Height).ToArray();
            double totalArea = _bmp.Width * _bmp.Height;
            double poreAreaPercentage;
            double coveredArea = 0;
            while (coveredArea < volume)
            {
                 validPores = validPores.Where(p => (p.Area / totalArea * 100) < volume - coveredArea).ToArray();
                PoreDto pore;
                if (validPores.Length == 0)
                {
                    pore = poresData.OrderBy(p => p.Area).First();
                }
                else
                {
                    var index = _random.Next(0, validPores.Length);
                    pore = validPores[index];
                }
                if (GenerateBlobs(pore, 1))
                {
                    poreAreaPercentage = (pore.Area / totalArea * 100);
                    coveredArea += poreAreaPercentage;
                    OnProgress(this, poreAreaPercentage);
                }
            }

            return _bmp;
        }

        private bool GenerateBlobs(PoreDto pore, int quantity)
        {
            pore.PoreImage.RotateFlip((RotateFlipType)_random.Next(0, 8));
            var bmp = pore.PoreImage;
            //  var rec = blobMomentum.Blob.Rectangle;
            Rectangle rectangle;
            Point rectangleCenter;
            for (int i = 0; i < 10; i++)
            {
                int x = _random.Next((bmp.Width / 2), _bmp.Width - bmp.Width / 2);
                int y = _random.Next((bmp.Height / 2), _bmp.Height - bmp.Height / 2);
                rectangleCenter = new Point(x, y);
                rectangle = new Rectangle(new Point(rectangleCenter.X - bmp.Width / 2, rectangleCenter.Y - bmp.Height / 2), new Size(bmp.Width, bmp.Height));
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
                // image.RotateFlip((RotateFlipType)_random.Next(0, 8));
                // g.DrawRectangle(Pens.Red, rectangle);
                g.DrawImage(image, rectangle.Location);
            }
        }
    }
}
