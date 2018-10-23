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
        public event EventHandler OnProgress;
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

        public Bitmap GenerateMicrostructure(List<PoreData> poresData)
        {
            var ordered = poresData.OrderByDescending(bm => bm.Blob.Area).GroupBy(bm => bm.ShapeId);
            foreach (var bm in ordered)
            {
                GenerateBlobs(poresData.First(e => e.ShapeId == bm.Key), bm.Count());
                OnProgress(this, new EventArgs());
            }

            return _bmp;
        }

        private void GenerateBlobs(PoreData blobMomentum, int quantity)
        {
            
            var rec = blobMomentum.Blob.Rectangle;
            Rectangle rectangle;
            Point rectangleCenter;
            for (int i = 0; i < quantity; i++)
            {
                do
                {
                    int x = _random.Next((rec.Width / 2), _bmp.Width - rec.Width / 2);
                    int y = _random.Next((rec.Height / 2), _bmp.Height - rec.Height / 2);
                    rectangleCenter = new Point(x, y);
                    rectangle = new Rectangle(new Point(rectangleCenter.X - rec.Width / 2, rectangleCenter.Y - rec.Height / 2), new Size(rec.Width, rec.Height));
                } while (!CheckRectangleIsClear(rectangle));

                DrawImage(rectangle, blobMomentum.Bmp);
            }
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
                image.RotateFlip((RotateFlipType)_random.Next(0, 8));
                g.DrawImage(image, rectangle.Location);
            }
        }
    }
}
