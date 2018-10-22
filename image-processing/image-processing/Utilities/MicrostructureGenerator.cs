using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace image_processing.Utilities
{
    public  class MicrostructureGenerator
    {
        public event EventHandler OnProgress;
        private Bitmap _bmp;
        public MicrostructureGenerator(int witdh,int height)
        {
            _bmp = new Bitmap(witdh, height,PixelFormat.Format32bppArgb);
           var bmpData = _bmp.LockBits(new Rectangle(0, 0, _bmp.Width, _bmp.Height), ImageLockMode.WriteOnly, _bmp.PixelFormat);
            var ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) *bmpData.Height;
            
            
            byte[] rgbValues = new byte[bytes].Select(val=> (byte)255).ToArray();
            Marshal.Copy(rgbValues,0,ptr,bytes);
            _bmp.UnlockBits(bmpData);
            
        }
        public  Bitmap GenerateMicrostructure(Dictionary<Guid,int> blobsQuantity, List<PoreData> list)
        {
           
           var ordered = list.OrderByDescending(bm => bm.Blob.Area).GroupBy(bm=>bm.ShapeId);

            foreach(var bm in ordered)
            {
                GenerateBlobs(list.First(e=>e.ShapeId==bm.Key), blobsQuantity[bm.Key]);
                OnProgress(this, new EventArgs());
            }

            return _bmp; 
        }

        private void GenerateBlobs (PoreData blobMomentum, int quantity)
        {
            
            Random random = new Random();
            var rec = blobMomentum.Blob.Rectangle;
            Rectangle rectangle;
            Point rectangleCenter;
            for (int i = 0; i < quantity; i++)
            {
                do
                {
                    int x = random.Next((rec.Width / 2), _bmp.Width - rec.Width / 2);
                    int y = random.Next((rec.Height / 2), _bmp.Height - rec.Height / 2);
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

        private void DrawImage(Rectangle rectangle,Bitmap image)
        {
            //var bmpData = _bmp.LockBits(new Rectangle(0, 0, _bmp.Width, _bmp.Height), ImageLockMode.ReadWrite, _bmp.PixelFormat);

            ////Graphics g = Graphics.FromImage(_bmp);
            ////g.DrawLines(Pens.Black, bm.GetEdgepointsDistancesFromCenterOfGravity().Select(t => new PointF(center.X + t.Item1, center.Y + t.Item2)).ToArray());
            //Drawing.Polygon(bmpData,
            //    bm.GetEdgepointsDistancesFromCenterOfGravity().Select(p => new AForge.IntPoint(center.X + p.Item1, center.Y + p.Item2)).ToList(),
            //    Color.Black);

            //_bmp.UnlockBits(bmpData);

            //var blobData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
            //var ptr = blobData.Scan0;
            //int bytes = Math.Abs(blobData.Stride) * image.Height;
            //byte[] rgbValues = new byte[bytes];
            //Marshal.Copy(ptr,rgbValues,0, bytes);
            //image.UnlockBits(blobData);

            //var bmpData = _bmp.LockBits(rectangle, ImageLockMode.WriteOnly, _bmp.PixelFormat);
            //Marshal.Copy(rgbValues, 0, bmpData.Scan0, bytes);
            //_bmp.UnlockBits(bmpData);


            using (var g = Graphics.FromImage(_bmp))
            {
                Random random = new Random(DateTime.Now.Millisecond);
                image.RotateFlip((RotateFlipType) random.Next(0,8));
                g.DrawImage(image, rectangle.Location);
            }
        }

       
    }
}
