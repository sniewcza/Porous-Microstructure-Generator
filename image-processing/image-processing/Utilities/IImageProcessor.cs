using System;
using System.Collections.Generic;
using System.Drawing;

namespace image_processing.Utilities
{
    public  interface IImageProcessor
    {
        event EventHandler<int> OnStart;
        event EventHandler OnProgress;
        event EventHandler OnEnd;
        Bitmap Binarization(Bitmap bitmap, int threshold);
        Bitmap Erosion(Bitmap bitmap);
        Bitmap Dilatation(Bitmap bitmap);
        Bitmap Opening(Bitmap bitmap);
        Bitmap Closing(Bitmap bitmap);
        Bitmap Skeletonization(Bitmap bitmap);
        Bitmap ReverseBitmapColors(Bitmap bitmap);
        List<PoreData> FindShapes(Bitmap bitmap);      
        double GetPoresVolume(Bitmap bitmap);
        List<int> BlobsArea(Bitmap bitmap);
        Bitmap ConvertToGrayscale(Bitmap original);
        Bitmap FilterBloobs(Bitmap bitmap, int minWidth, int minHeight);
    }
}
