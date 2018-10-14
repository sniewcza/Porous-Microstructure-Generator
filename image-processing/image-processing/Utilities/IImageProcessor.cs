using AForge.Imaging;
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
        void FindShapes(Bitmap bitmap);
        Blob GetBlobAtPixel(int x, int y);
        double GetPoresVolume(Bitmap bitmap);
        List<int> BlolbsArea(Bitmap bitmap);
        List<BlobMomentum> BlobsMomentum();
        Bitmap ConvertTo16bpp(Bitmap original);
    }
}
