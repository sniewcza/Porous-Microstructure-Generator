using System;
using System.Collections.Generic;
using System.Linq;

namespace image_processing.Utilities
{
    [Serializable]
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
    [Serializable]
    public class ShapeAnalyzer
    {
        public event EventHandler<int> ShapeCountChange;
        private Dictionary<double[], Guid> _shapeDictionary;
        // private readonly double _similarityCoefficient;
        private IShapeClassifier _classifier;

        public ShapeAnalyzer(double similarityCoefficient)
        {
            _shapeDictionary = new Dictionary<double[], Guid>(new CustomComparer());
            _classifier = new KNNClassifier(similarityCoefficient, 3);

        }

        public ShapeAnalyzer(ShapeAnalyzer shapeAnalyzer, double similarityCoefficient)
        {
            this._shapeDictionary = shapeAnalyzer.ShapeDictionary;
            _classifier = new KNNClassifier(similarityCoefficient, 3);
        }

        public Dictionary<double[], Guid> ShapeDictionary
        {
            get => _shapeDictionary;
            set
            {
                _shapeDictionary = value;
                ShapeCountChange?.Invoke(this, _shapeDictionary.Count);
            }
        }

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
                ShapeCountChange?.Invoke(this, _shapeDictionary.Count);
                return shapeId;
            }
            else
            {
                Guid shapeId = _classifier.Classify(shapeDescriptor, _shapeDictionary);

                if (!_shapeDictionary.ContainsValue(shapeId))
                {
                    _shapeDictionary.Add(shapeDescriptor, shapeId);
                    ShapeCountChange?.Invoke(this, _shapeDictionary.Count);
                }

                return shapeId;
            }
        }


    }
}
