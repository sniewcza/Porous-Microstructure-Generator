using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using image_processing.Utilities;
using image_processing.View;
using System.Linq;
using System.Threading.Tasks;

namespace image_processing
{
    public partial class Form1 : Form
    {
        Utilities.Image _image;
        IImageProcessor _processor;
        ShapeAnalyzer shapeAnalyzer;
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
                var bm = _processor.BlobsMomentum().First(blobinfo => blobinfo.Blob.ID == blob.ID);
                BlobView form = new BlobView(blob,shapeAnalyzer,bm);
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
                var bmp = _processor.ConvertTo16bpp(new Bitmap(ImageFileDialog.FileName));
               // bmp = _processor.ReverseBitmapColors(bmp);
                _image.OriginalImage = bmp; //new Bitmap(ImageFileDialog.FileName);
                _image.ProcessingImage = bmp;//new Bitmap(ImageFileDialog.FileName);
                _image.ViewImage = bmp;//new Bitmap(ImageFileDialog.FileName);                                  
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
            MessageBox.Show($"Before {_processor.GetPoresVolume(_processor.Binarization(_image.OriginalImage,60))} \n After {_processor.GetPoresVolume(_image.ViewImage)}");
        }

        private void sizeDistributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> sizes = _processor.BlolbsArea(_image.ProcessingImage);
            
            SizeDistributionView distributionView = new SizeDistributionView(sizes);

            distributionView.Show();
        }

        private async void generateMicrostructureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {

                GeneratorProgressBar progressBar = new GeneratorProgressBar();

                _processor.OnStart += (s, blobsNumber) =>
                 {
                     progressBar.setMaxValue( blobsNumber*2);
                 };

                _processor.OnProgress += (s, ea) =>
                  {
                      progressBar.Increment();
                  };

                progressBar.Info = "Finding shapes";
                progressBar.Show();
                
                await Task.Run( ()=>_processor.FindShapes(_image.ProcessingImage));

                progressBar.Info = "Analyzing shapes";

                shapeAnalyzer = new ShapeAnalyzer();
                Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
                List<BlobMomentum> list = _processor.BlobsMomentum();

                await Task.Run(() =>
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        var bm = list[i];
                        var shapeId = shapeAnalyzer.Analyze(bm.getShapeDescriptor());
                        bm.Guid1 = shapeId;
                        if (dictionary.ContainsKey(shapeId))
                        {
                            dictionary[shapeId]++;
                        }
                        else
                        {
                            dictionary.Add(shapeId, 1);
                        }

                        progressBar.Increment();
                    }
                });

               // progressBar.ProgressBar1.Maximum += dictionary.Values.Sum();
                progressBar.Info = "Generating microstructure";
                // _image.ViewImage = bmp;
                MicrostructureGenerator generator = new MicrostructureGenerator(_image.OriginalImage.Width, _image.OriginalImage.Height);
                
                
                generator.OnProgress += (s, eh) =>
                {
                    progressBar.Increment();
                };

                
               var bmp = await Task.Run(() => generator.GenerateMicrostructure(dictionary, list));
                progressBar.Dispose();
                bmp = _processor.ConvertTo16bpp(bmp);
                _image.ViewImage = bmp;
                //   _processor.Closing(bmp);
                //  _image.ProcessingImage = bmp;
            }
        }

        private void exportShapeAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = new FileStream(fileDialog.FileName, FileMode.Create, FileAccess.Write);
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, shapeAnalyzer.TrainingData);
                stream.Close();

            }
        }
    }
}
