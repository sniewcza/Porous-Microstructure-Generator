using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using image_processing.Utilities;
using image_processing.View;

namespace image_processing
{
    public partial class Form1 : Form
    {
        Utilities.Image _image;
        IImageProcessor _processor;
        public Form1(IImageProcessor imageProcessor)
        {
            InitializeComponent();         
            _processor = imageProcessor;
            _image = new Utilities.Image();
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
                    
                

            }
        }

        private void binarizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var form = new Form2();
                form.ShowDialog();
                var bmp = _processor.Binarization(_image.ViewImage,128);
                _image.ProcessingImage = bmp;
                _image.ViewImage = bmp;
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                _image.ViewImage = _processor.ReverseBitmapColors(_image.ViewImage);
            }
        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Erosion(_image.ViewImage);
                _image.ViewImage = bmp;
                _image.ProcessingImage = bmp;
            }
        }

        private void dilatationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Dilatation(_image.ViewImage);
                _image.ViewImage = bmp;
                _image.ProcessingImage = bmp;
            }
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Opening(_image.ViewImage);
                _image.ViewImage = bmp;
                _image.ProcessingImage = bmp;
            }
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Closing(_image.ViewImage);
                _image.ViewImage = bmp;
                _image.ProcessingImage = bmp;
            }
        }

        private void skeletonizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Skeletonization(_image.ViewImage);
                _image.ViewImage = bmp;
                _image.ProcessingImage = bmp;
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _image.OriginalImage;
                _image.ViewImage = bmp;
                _image.ProcessingImage = bmp;
            }
        }
    }
}
