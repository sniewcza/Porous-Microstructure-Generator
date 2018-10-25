using System;
using System.Drawing;
namespace image_processing.Utilities
{
    class Image
    {  
        public event EventHandler OnViewImageChange;
        private Bitmap _viewImage;
        public Bitmap OriginalImage { get; set; }
        public Bitmap ViewImage
        {
            get
            {
                return _viewImage;
            }
            set
            {
                _viewImage = value;
                OnViewImageChange(this, new EventArgs());
            }
        }

        public Image()
        {

        }
    }
}
