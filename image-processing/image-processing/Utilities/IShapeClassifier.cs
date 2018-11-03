using System;
using System.Collections.Generic;

namespace Generator.Utilities
{
    interface IShapeClassifier
    {
        Guid Classify(double[] input, Dictionary<double[], Guid> trainingSet);
    }
}
