using AForge.Imaging;
using AForge.Math;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;

namespace image_processing.Utilities
{
    public class PoorData
    {
        private AForge.IntPoint _centerOfgravity;
        private Guid _shapeId;     
        private Blob _blob;
        private Bitmap _bmp;
        private List<AForge.IntPoint> _edgePoints;
       
        private double _centralX;
        private double _centralY;
        public double M1 { get; set; }
        public double M2 { get; set; }
        public double M3 { get; set; }     
        public double M7 { get; set; }
        public double Lp1 { get; set; }
        public double CentralX { get => _centralX;  }
        public double CentralY { get => _centralY;  }
        public Bitmap Bmp { get => _bmp; }
       
        private double _area;
        public double M { get; set; }         
        public double Area { get => _area; set => _area = value; }
        public Guid ShapeId { get => _shapeId; set => _shapeId = value; }
        public Blob Blob { get => _blob;}

        public PoorData(Blob blob,Bitmap bitmap, List<AForge.IntPoint> edgePoints)
        {
            this._blob = blob;
            this._centerOfgravity = blob.CenterOfGravity.Round(); ;          
            this._area = blob.Area;
            _bmp = bitmap;
            _edgePoints = edgePoints;
         

            double m00 = getMomentum(0, 0);

            _centralX = getMomentum(1, 0) /m00;
            _centralY = getMomentum(0, 1) / m00;
            M1 = (getCentralMomentum(2, 0) + getCentralMomentum(0, 2)) / Math.Pow(m00, 2);

            M2 = (Math.Pow(getCentralMomentum(2, 0) + getCentralMomentum(0, 2), 2) + 4 * Math.Pow(getCentralMomentum(1, 1), 2)) / Math.Pow(m00, 4);

            M3 = (Math.Pow(getCentralMomentum(3, 0) + 3 * getCentralMomentum(1, 2), 2) + Math.Pow(3 * getCentralMomentum(2, 1) - getCentralMomentum(0, 3), 2)) / Math.Pow(m00, 5);

           // M4 = (Math.Pow(getCentralMomentum(3, 0) + getCentralMomentum(1, 2), 2) + Math.Pow(getCentralMomentum(2, 1) - getCentralMomentum(0, 3), 2)) / Math.Pow(m00, 5);

           // M5 = ((getCentralMomentum(3, 0) - 3 * getCentralMomentum(1, 2)) * (getCentralMomentum(3, 0) + getCentralMomentum(1, 2)) * (Math.Pow(getCentralMomentum(3, 0) + getCentralMomentum(1, 2), 2) - 3 * (Math.Pow(getCentralMomentum(2, 1) + getCentralMomentum(0, 3), 2))) +

             //   (3 * getCentralMomentum(2, 1) - getCentralMomentum(0, 3)) * (getCentralMomentum(2, 1) + getCentralMomentum(0, 3)) * (3 * Math.Pow(getCentralMomentum(3, 0) + getCentralMomentum(1, 2), 2) - Math.Pow(getCentralMomentum(2, 1) + getCentralMomentum(0, 3), 2))) / Math.Pow(m00, 10);

          //  M6 = ((getCentralMomentum(2, 0) - getCentralMomentum(0, 2)) * (Math.Pow(getCentralMomentum(3, 0) + getCentralMomentum(1, 2), 2) - Math.Pow(getCentralMomentum(2, 1) + getCentralMomentum(0, 3), 2)) +

             //   4 * getCentralMomentum(1, 1) * (getCentralMomentum(3, 0) + getCentralMomentum(1, 2)) * (getCentralMomentum(2, 1) + getCentralMomentum(0, 3))) / Math.Pow(m00, 7);

            M7 = (getCentralMomentum(2, 0) * getCentralMomentum(0, 2) - Math.Pow(getCentralMomentum(1, 1), 2)) / Math.Pow(m00, 4);

           // ComputeLp1();
            ComputeM();
         
        }

        
        private double getMomentum(int Ptier, int Qtier)
        {        
            double momentum = 0;
            for (int i = 0; i < _bmp.Width; i++)
                for (int j = 0; j < _bmp.Height; j++)
                    momentum += (_bmp.GetPixel(i, j).ToArgb() == Color.Black.ToArgb() ? 1 : 0) * Math.Pow(i, Ptier) * Math.Pow(j, Qtier);

            return momentum;
        }

        

        private double getCentralMomentum(int Ptier, int Qtier)
        {
            int black = Color.Black.ToArgb();
            double momentum = 0;
            for (int i = 0; i < _bmp.Width; i++)
                for (int j = 0; j < _bmp.Height; j++)
                    momentum += (_bmp.GetPixel(i, j).ToArgb() == black ? 1 : 0) * Math.Pow(i-CentralX, Ptier) * Math.Pow(j-CentralY, Qtier);

            return momentum;
        }

        public double[] getShapeDescriptor()
        {
            return new double[] { M1,M2,M3,M7,Area,M};
        }

        private void ComputeLp1()
        {
            float rmin = int.MaxValue, rmax = 0;
            
            foreach (AForge.IntPoint point in _edgePoints)
            {
                float distance = point.SquaredDistanceTo(this.Blob.CenterOfGravity);
                if (distance > rmax)
                    rmax = distance;
                if (distance < rmin)
                    rmin = distance;
            }
            Lp1 = rmin / rmax;
        }

        private void ComputeM()
        {
            double m = 0.5 * _edgePoints.Count / Math.Sqrt(3.14 * Area) - 1;

            M = m < 0 ? 0 : m;
        }

       
    }
}
