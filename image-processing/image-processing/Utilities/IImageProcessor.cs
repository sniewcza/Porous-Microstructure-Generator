﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_processing.Utilities
{
  public  interface IImageProcessor
    {
        Bitmap Binarization(Bitmap bitmap, int threshold);
        Bitmap Erosion(Bitmap bitmap);
        Bitmap Dilatation(Bitmap bitmap);
        Bitmap Opening(Bitmap bitmap);
        Bitmap Closing(Bitmap bitmap);
        Bitmap Skeletonization(Bitmap bitmap);
        Bitmap ReverseBitmapColors(Bitmap bitmap);
        Bitmap FindShapes(Bitmap bitmap);
        Bitmap GetBlobAtPixel(int x, int y);
        double GetPoresVolume(Bitmap bitmap);
        List<int> BlolbsArea(Bitmap bitmap);
    }
}
