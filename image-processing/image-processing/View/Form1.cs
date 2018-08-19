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
            pictureBox1.MouseClick += PictureBox1_MouseClick;

        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var blob = _processor.GetBlobAtPixel(e.X, e.Y);
            if (blob != null)
            {
                BlobView form = new BlobView(blob);
                form.Show();
            }
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
                var form = new ThresholdInputBox(_image.OriginalImage);
                form.ThresholdChanged += (textbox, args) =>
                {
                    int threshold = Convert.ToInt32((textbox as MaskedTextBox).Text);
                   form.PictureBox1.Image = _image.ProcessingImage = _processor.Binarization(_image.OriginalImage, threshold);
                };
               if( form.ShowDialog() == DialogResult.OK)
                {
                    _image.ViewImage = (_image.ProcessingImage.Clone() as Bitmap);
                }

                form.Dispose();
            }
        }

       

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Erosion(_image.ProcessingImage);
                _image.ViewImage = bmp;
                _image.ProcessingImage = bmp;
            }
        }

        private void dilatationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Dilatation(_image.ProcessingImage);
                _image.ViewImage = bmp;
                _image.ProcessingImage = bmp;
            }
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Opening(_image.ProcessingImage);
                _image.ViewImage = bmp;
                _image.ProcessingImage = bmp;
            }
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Closing(_image.ProcessingImage);
                _image.ViewImage = bmp;
                _image.ProcessingImage = bmp;
            }
        }

        private void skeletonizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Skeletonization(_image.ProcessingImage);
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

        private void blobDetectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.FindShapes(_image.ProcessingImage);
                _image.ViewImage = bmp;
               // _image.ProcessingImage = bmp;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "bmp";
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _image.ViewImage.Save(saveFileDialog.FileName);
            }
        }

        private void volumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Before {_processor.GetPoresVolume(_image.OriginalImage)} \n After {_processor.GetPoresVolume(_image.ViewImage)}");
        }

        private void sizeDistributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> sizes = _processor.BlolbsArea(_image.ProcessingImage);
            
            SizeDistributionView distributionView = new SizeDistributionView(sizes);

            distributionView.Show();
        }
    }
}
