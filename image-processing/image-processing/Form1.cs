using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace image_processing
{
    public partial class Form1 : Form
    {
        Utilities.Image _image;
        public Form1()
        {
            InitializeComponent();
            _image = new Utilities.Image(new Utilities.ImageProcessor());
            _image.OnViewImageChange += _image_OnViewImageChange;
        }

        private void _image_OnViewImageChange(object sender, EventArgs e)
        {
            this.pictureBox1.Image = _image.ViewImage;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                _image.OriginalImage = new Bitmap(ImageFileDialog.FileName);
                _image.ProcessingImage = new Bitmap(ImageFileDialog.FileName);
                _image.ViewImage = new Bitmap(ImageFileDialog.FileName);
               // pictureBox1.Image = _image.ViewImage;
            }
        }

        private void binarizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var bmp = _image.Processor.Binarization(_image.ViewImage, 128);
            _image.ProcessingImage = bmp;
            _image.ViewImage = bmp;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
           _image.ViewImage= _image.Processor.ReverseBitmapColors(_image.ViewImage);
        }
    }
}
