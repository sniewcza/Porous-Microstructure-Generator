using AForge.Math.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace image_processing.Utilities
{
    class KNNClassifier : IShapeClassifier
    {
        private ISimilarity _similarity;
        private readonly double _similarityCoefficient;
        private readonly int _neighboursToConsider;

        public KNNClassifier(double similarityCoefficient, int kCoefficient)
        {
            _similarity = new EuclideanSimilarity();
            _similarityCoefficient = similarityCoefficient;
            _neighboursToConsider = kCoefficient;
        }

        public Guid Classify(double[] input, Dictionary<double[], Guid> trainingSet)
        {         
            Dictionary<double[],double> inputSimilarityToTrainingSet = CalculateSimilarity(input,trainingSet);
            var aboveSimilarityThreshold = inputSimilarityToTrainingSet.Where(pair => pair.Value > _similarityCoefficient).ToList();

            if (aboveSimilarityThreshold.Count == 0)
            {
                return Guid.NewGuid();
            }
            else
            {
                var nearestNeighbours = aboveSimilarityThreshold.OrderByDescending(pair => pair.Value).Select(pair => pair.Key).Take(_neighboursToConsider).ToList();
                var NeighboursToVote = nearestNeighbours.Join(trainingSet, pair => pair, pair2 => pair2.Key, (pair, pair2) => pair2.Value);
                var votes = NeighboursToVote.GroupBy(ShapeClass => ShapeClass);
                return votes.FirstOrDefault(g => g.Count() == votes.Max(gr => gr.Count())).Key;
            }
        }

        private Dictionary<double[], double> CalculateSimilarity(double[] input , Dictionary<double[], Guid> trainingSet)
        {
            Dictionary<double[], double> similarities = new Dictionary<double[], double>();

            foreach (double[] val in trainingSet.Keys)
            {
                double distance = _similarity.GetSimilarityScore(input, val);
                similarities.Add(val, distance);
            }
            return similarities;
        }
    }
}

