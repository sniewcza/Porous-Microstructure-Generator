using System;
using System.Drawing;
namespace image_processing.Utilities
{
    class Image
    {
        private Bitmap _ViewImage;
        public event EventHandler OnViewImageChange;
        public Bitmap OriginalImage { get; set; }
        public Bitmap ViewImage
        {
            get
            {
                return _ViewImage;
            }
            set
            {
                _ViewImage = value;
                OnViewImageChange(this, new EventArgs());
            }
        }

        public Image()
        {

        }
    }
}
