using System;
using System.Collections.Generic;

namespace image_processing.Utilities
{
    interface IShapeClassifier
    {
        Guid Classify(double[] input, Dictionary<double[], Guid> trainingSet);
    }
}
