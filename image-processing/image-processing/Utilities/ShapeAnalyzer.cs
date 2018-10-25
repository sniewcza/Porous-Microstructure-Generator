using System;
using System.Collections.Generic;
using System.Linq;

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
            foreach (double val in obj)
            {
                hash += val.GetHashCode();
            }
            return hash;
        }
    }

    public class ShapeAnalyzer
    {
        private Dictionary<double[], Guid> _shapeDictionary;
        private readonly double _similarityCoefficient;
        private IShapeClassifier _classifier;
        public ShapeAnalyzer(double similarityCoefficient)
        {
            _shapeDictionary = new Dictionary<double[], Guid>(new CustomComparer());
            _similarityCoefficient = similarityCoefficient;
            _classifier = new KNNClassifier(similarityCoefficient, 3);
        }

        public Dictionary<double[], Guid> ShapeDictionary { get => _shapeDictionary; internal set => _shapeDictionary = value; }

        public void AddTrainingData(double[] shapeDescriptor, Guid description)
        {
            _shapeDictionary.Add(shapeDescriptor, description);
        }
        public Guid Analyze(double[] shapeDescriptor)
        {
            if (shapeDescriptor.Length != 6)
            {
                throw new Exception("Shepe Descriptor must be length of 6");
            }

            if (_shapeDictionary.ContainsKey(shapeDescriptor))
            {
                return _shapeDictionary[shapeDescriptor];
            }

            if (_shapeDictionary.Keys.Count == 0)
            {
                Guid shapeId = Guid.NewGuid();
                _shapeDictionary.Add(shapeDescriptor, shapeId);
                return shapeId;
            }
            else
            {
                Guid shapeId = _classifier.Classify(shapeDescriptor, _shapeDictionary);

                if (!_shapeDictionary.ContainsValue(shapeId))
                {
                    _shapeDictionary.Add(shapeDescriptor, shapeId);
                }

                return shapeId;
            }
        }


    }
}
