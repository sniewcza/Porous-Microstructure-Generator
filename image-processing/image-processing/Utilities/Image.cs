using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Bitmap ProcessingImage { get; internal set; }
        public IImageProcessor Processor { get => _processor; }
        IImageProcessor _processor;
        public Image(IImageProcessor processor)
        {
            _processor = processor;
        }
    }
}
