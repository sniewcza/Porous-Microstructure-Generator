using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Generator.Model
{
    [Serializable]
    public class PoreAnalyzeData
    {
        public double GeometricMoment1 { get; private set; }
        public double GeometricMoment2 { get; private set; }
        public double GeometricMoment3 { get; private set; }
        public double GeometricMoment7 { get; private set; }
        public double Lp1 { get; private set; }
        public double CentralX { get; private set; }
        public double CentralY { get; private set; }
        public Bitmap Bmp { get; private set; }
        public double MalinowskasCoefficient { get; private set; }
        public double Area { get; private set; }
        public Guid ShapeId { get; set; }
        public Blob Blob { get; private set; }
        private int[,] _bmpData;
        private List<AForge.IntPoint> _edgePoints;
        private double m00;
        public PoreAnalyzeData(Bitmap poreImage, Guid shapeId, double shapeArea)
        {
            Bmp = poreImage;
            ShapeId = shapeId;
            Area = shapeArea;
        }

        public PoreAnalyzeData(Blob blob, Bitmap bitmap, List<AForge.IntPoint> edgePoints)
        {
            Blob = blob;
            Area = blob.Area;
            Bmp = bitmap;
            _edgePoints = edgePoints;
            _bmpData = InitializeBmpData(bitmap);
            m00 = getMomentum(0, 0);
            CentralX = getMomentum(1, 0) / m00;
            CentralY = getMomentum(0, 1) / m00;

            GeometricMoment1 = ComputeGeometricMoment1();

            GeometricMoment2 = ComputeGeometricMoment2();

            GeometricMoment3 = ComputeGeometricMoment3();

            // M4 = (Math.Pow(getCentralMomentum(3, 0) + getCentralMomentum(1, 2), 2) + Math.Pow(getCentralMomentum(2, 1) - getCentralMomentum(0, 3), 2)) / Math.Pow(m00, 5);

            // M5 = ((getCentralMomentum(3, 0) - 3 * getCentralMomentum(1, 2)) * (getCentralMomentum(3, 0) + getCentralMomentum(1, 2)) * (Math.Pow(getCentralMomentum(3, 0) + getCentralMomentum(1, 2), 2) - 3 * (Math.Pow(getCentralMomentum(2, 1) + getCentralMomentum(0, 3), 2))) +

            //   (3 * getCentralMomentum(2, 1) - getCentralMomentum(0, 3)) * (getCentralMomentum(2, 1) + getCentralMomentum(0, 3)) * (3 * Math.Pow(getCentralMomentum(3, 0) + getCentralMomentum(1, 2), 2) - Math.Pow(getCentralMomentum(2, 1) + getCentralMomentum(0, 3), 2))) / Math.Pow(m00, 10);

            //  M6 = ((getCentralMomentum(2, 0) - getCentralMomentum(0, 2)) * (Math.Pow(getCentralMomentum(3, 0) + getCentralMomentum(1, 2), 2) - Math.Pow(getCentralMomentum(2, 1) + getCentralMomentum(0, 3), 2)) +

            //   4 * getCentralMomentum(1, 1) * (getCentralMomentum(3, 0) + getCentralMomentum(1, 2)) * (getCentralMomentum(2, 1) + getCentralMomentum(0, 3))) / Math.Pow(m00, 7);

            GeometricMoment7 = ComputeGeometricMoment7();


            MalinowskasCoefficient = ComputeM();

        }

        private int[,] InitializeBmpData(Bitmap bitmap)
        {
            int[,] bmpData = new int[bitmap.Width, bitmap.Height];

            for (int i = 0; i < bitmap.Width; i++)
                for (int j = 0; j < bitmap.Height; j++)
                    bmpData[i, j] = bitmap.GetPixel(i, j).ToArgb() == Color.Black.ToArgb() ? 1 : 0;

            return bmpData;
        }

        private double getMomentum(int Ptier, int Qtier)
        {
            double momentum = 0;
            for (int i = 0; i < Bmp.Width; i++)
                for (int j = 0; j < Bmp.Height; j++)
                    momentum += _bmpData[i, j] * Math.Pow(i, Ptier) * Math.Pow(j, Qtier);

            return momentum;
        }

        private double getCentralMomentum(int Ptier, int Qtier)
        {
            double momentum = 0;
            for (int i = 0; i < Bmp.Width; i++)
                for (int j = 0; j < Bmp.Height; j++)
                    momentum += _bmpData[i, j] * Math.Pow(i - CentralX, Ptier) * Math.Pow(j - CentralY, Qtier);

            return momentum;
        }

        public double[] getShapeDescriptor()
        {
            return new double[] { GeometricMoment1, GeometricMoment2, GeometricMoment3, GeometricMoment7, Area, MalinowskasCoefficient };
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

        private double ComputeM()
        {
            double m = 0.5 * _edgePoints.Count / Math.Sqrt(3.14 * Area) - 1;

            return m < 0 ? 0 : m;
        }

        private double ComputeGeometricMoment1()
        {
            return (getCentralMomentum(2, 0) + getCentralMomentum(0, 2)) / Math.Pow(m00, 2);
        }

        private double ComputeGeometricMoment2()
        {
            return (Math.Pow(getCentralMomentum(2, 0) + getCentralMomentum(0, 2), 2) + 4 * Math.Pow(getCentralMomentum(1, 1), 2)) / Math.Pow(m00, 4);
        }

        private double ComputeGeometricMoment3()
        {
            return (
                    Math.Pow(getCentralMomentum(3, 0) + 3 * getCentralMomentum(1, 2), 2) +
                    Math.Pow(3 * getCentralMomentum(2, 1) - getCentralMomentum(0, 3), 2)
                    )
                        / Math.Pow(m00, 5);
        }

        private double ComputeGeometricMoment7()
        {
           return (getCentralMomentum(2, 0) * getCentralMomentum(0, 2) - Math.Pow(getCentralMomentum(1, 1), 2)) / Math.Pow(m00, 4);
        }
    }
}
