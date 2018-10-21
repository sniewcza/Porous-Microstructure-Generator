using System;
using System.Collections.Generic;
using System.Linq;
using AForge.Math.Metrics;
namespace image_processing.Utilities
{
    
    class CustomComparer : IEqualityComparer<double[]>
    {
        public bool Equals(double[] x, double[] y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(double[] obj)
        {
            int hash = 0;
            foreach(double val in obj)
            {
                hash += val.GetHashCode();
            }
            return hash;
        }
    }
   
    public class ShapeAnalyzer
    {
        private EuclideanDistance EuclideanDistance;
        private Dictionary<double[], Guid> trainingData;
        private double _similarityCoefficient;

        public ShapeAnalyzer(double similarityCoefficient)
        {
            EuclideanDistance = new EuclideanDistance();
            trainingData = new Dictionary<double[], Guid>(new CustomComparer());
            _similarityCoefficient = similarityCoefficient;
        }

        public Dictionary<double[],Guid> TrainingData { get => trainingData; set => trainingData = value; }

        public void AddTrainingData(double[] shapeDescriptor, Guid description)
        {
            trainingData.Add(shapeDescriptor, description);
        }
        public Guid Analyze(double[] shapeDescriptor)
        {
            if(shapeDescriptor.Length != 6)
            {
                throw new System.Exception("Shepe Descriptor must be length of 6");
            }          
            if (trainingData.ContainsKey(shapeDescriptor))
            {
                return trainingData[shapeDescriptor];
            }
            if(trainingData.Keys.Count == 0)
            {
                Guid guid = Guid.NewGuid();
                trainingData.Add(shapeDescriptor, guid);
                return guid;
            }
            else
            {
                
                var distances = CalculateDistances(shapeDescriptor);

                var similar = distances.Where(pair => pair.Value > _similarityCoefficient).ToList();
                if (similar.Count == 0)
                {
                    Guid guid = Guid.NewGuid();
                    trainingData.Add(shapeDescriptor, guid);
                    return guid;
                }
                else
                {
                    var NN = similar.OrderByDescending(pair => pair.Value).Select(pair => pair.Key).Take(3).ToList();


                    var v = NN.Join(trainingData, pair => pair, pair2 => pair2.Key, (pair, pair2) => pair2.Value);
                    var x = v.GroupBy(ShapeClass => ShapeClass);
                    return x.FirstOrDefault(g => g.Count() == x.Max(gr => gr.Count())).Key;
                }
            }
        }

        private Dictionary<double[],double> CalculateDistances(double[] shapeDescriptor)
        {
            Dictionary<double[], double> distances = new Dictionary<double[], double>();
            ISimilarity similarity = new EuclideanSimilarity();

            foreach(double[] val in trainingData.Keys)
            {
                double distance = similarity.GetSimilarityScore(shapeDescriptor, val);
                distances.Add(val,distance);
            }
            return distances;
        }
    }
}
