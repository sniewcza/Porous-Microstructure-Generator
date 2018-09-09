using System.Collections.Generic;
using System.Linq;
using AForge.Math.Metrics;
namespace image_processing.Utilities
{
    
    class ShapeAnalyzer
    {
        private EuclideanDistance EuclideanDistance;
        private Dictionary<double[], string> trainingData;
        double[] circle = { 0.16191, 0.02621, 0, 0.00632, 0.64, 0 };
        double[] circle2 = { 0.16711, 0.02792, 0.00003, 0.00635, 0.47299, 0 };
        double[] square = { 0.16798, 0.02821, 0.00002, 0.00705, 0.38281, 0.05812 };
        double[] square2 = { 0.16799, 0.02822, 0.00003, 0.00695, 0.40399, 0.10290 };
        double[] triangle = { 0.23103, 0.05337, 0.00422, 0.0087, 0.0882, 0.0842 };
        double[] triangle2 = { 0.20758, 0.05179, 0.00196, 0.00838, 0.13442, 0.04494 };
        double[] diamond = { 0.19637, 0.03856, 0.00012, 0.00654, 0.1849, 0 };
        double[] diamond2 = { 0.18131, 0.03287, 0.00019, 0.00683, 0.256630, 0 };

        public ShapeAnalyzer()
        {
            EuclideanDistance = new EuclideanDistance();
            trainingData = new Dictionary<double[], string>
            {
                { circle, "circle" },
                { circle2, "circle" },
                { square, "square" },
                { square2, "square" },
                { triangle, "triangle" },
                { triangle2, "triangle" },
                { diamond, "diamond" },
                { diamond2, "diamond" }
            };
        }

        public string Analyze(double[] shapeDescriptor)
        {
            if(shapeDescriptor.Length != 6)
            {
                throw new System.Exception("Shepe Descriptor must be length of 7");
            }

            var distances = CalculateDistances(shapeDescriptor,3);

           var NN = distances.OrderBy(pair=> pair.Value).Select(pair=>pair.Key).Take(3).ToList() ;

          var v =  trainingData.Join(NN, pair => pair.Key, pair2 => pair2, (pair, pair2) => pair.Value );
            var x = v.GroupBy(ShapeClass => ShapeClass);
            return x.First(g => g.Count() == x.Max(gr => gr.Count())).Key;
        }

        private Dictionary<double[],double> CalculateDistances(double[] shapeDescriptor,int numberOfNeigbhours)
        {
            Dictionary<double[], double> distances = new Dictionary<double[], double>();
            
            foreach(double[] val in trainingData.Keys)
            {
                double distance = EuclideanDistance.GetDistance(shapeDescriptor, val);
                distances.Add(val,distance);
            }
            return distances;
        }
    }
}
