using System;
using System.Collections.Generic;
using System.Drawing;
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
        GlobalSettings globalSettings;
        GeneratorProgressBar progressBar;

        public Form1(IImageProcessor imageProcessor)
        {
            InitializeComponent();         
            _processor = imageProcessor;
            _image = new Utilities.Image();
            _image.OnViewImageChange += _image_OnViewImageChange;
            pictureBox1.MouseClick += PictureBox1_MouseClick;
            globalSettings = new GlobalSettings()
            {
                SimilarityCoefficient = 0.2
            };

            _processor.OnStart += (s, blobsNumber) =>
            {
                progressBar.setMaxValue(blobsNumber * 2);
            };

            _processor.OnProgress += (s, ea) =>
            {
                progressBar.Increment();
            };

            this.SizeChanged += Form1_ResizeEnd;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if(_image.ViewImage != null)
            {
               RescalePictureBox();
            }
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
                var bmp = _processor.ConvertToGrayscale(new Bitmap(ImageFileDialog.FileName));
               
                _image.OriginalImage = bmp; 
                _image.ProcessingImage = bmp;
                _image.ViewImage = bmp;

                pictureBox1.BorderStyle = BorderStyle.FixedSingle;                
                EnableMenuBarToolstrips();
                RescalePictureBox();
            }
        }

        private void RescalePictureBox()
        {
            int pictrueWidth = _image.ViewImage.Width;
            int pictrueHeight = _image.ViewImage.Height;
            int panelWidth = this.panel1.Width;
            int panelHeight = this.panel1.Height;

            int verticalPadding = 0;
            int horizontalPadding = 0;
            if (pictrueWidth < panelWidth)
            {
                 horizontalPadding = (panelWidth -pictrueWidth) / 2;
               
            }
           
            if(pictrueHeight < panelHeight)
            {
                 verticalPadding = (panelHeight - pictrueHeight) / 2;            
            }

            panel1.Padding = new Padding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
        }
        private void EnableMenuBarToolstrips()
        {
            this.filtersToolStripMenuItem.Enabled = true;
            this.shapeToolStripMenuItem.Enabled = true;
            this.generatorToolStripMenuItem.Enabled = true;
            this.statisticToolStripMenuItem.Enabled = true;
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

                progressBar = new GeneratorProgressBar
                {
                    Info = "Finding shapes"
                };
                progressBar.Show();
                
                await Task.Run( ()=>_processor.FindShapes(_image.ProcessingImage));

                progressBar.Info = "Analyzing shapes";

                shapeAnalyzer = new ShapeAnalyzer(globalSettings.SimilarityCoefficient);
                Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
                List<PoreData> list = _processor.BlobsMomentum();

                await Task.Run(() =>
                {

                    if (globalSettings.NormalizeBlobArea)
                    {
                        var areas = list.Select(item => item.Area);
                        double max = areas.Max();
                        double min = areas.Min();
                        double range = max - min;
                        list.ForEach(data => data.Area = (data.Area - min) / range);
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        var bm = list[i];
                        var shapeId = shapeAnalyzer.Analyze(bm.getShapeDescriptor());
                        bm.ShapeId = shapeId;
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

              
                progressBar.Info = "Generating microstructure";
                
                MicrostructureGenerator generator = new MicrostructureGenerator(_image.OriginalImage.Width, _image.OriginalImage.Height);
                
                
                generator.OnProgress += (s, eh) =>
                {
                    progressBar.Increment();
                };

                
               var bmp = await Task.Run(() => generator.GenerateMicrostructure(dictionary, list));
                progressBar.Dispose();
                bmp = _processor.ConvertToGrayscale(bmp);
                _image.ViewImage = bmp;
              
            }
        }

       

        private void filterBlobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_image.ViewImage != null)
            {
                FilterBlobsInputBox form = new FilterBlobsInputBox();
                if(DialogResult.OK == form.ShowDialog())
                {
                    var bmp = _processor.FilterBloobs(_image.ProcessingImage, form.minWidth, form.minHeight);

                    _image.ViewImage = bmp;

                    form.Dispose();
                }

                
            }
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShapeAnalyzerSettingsView form = new ShapeAnalyzerSettingsView(globalSettings);
            
            if(form.ShowDialog() == DialogResult.OK)
            {
                globalSettings.SimilarityCoefficient = form.SimilarityCoefficient;
                globalSettings.NormalizeBlobArea = form.NormalizePoreArea;
            }
        }

       

        private async void analyzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBar = new GeneratorProgressBar
            {
                Info = "Finding shapes"
            };
            progressBar.Show();
            await Task.Run(() => _processor.FindShapes(_image.ViewImage));
            shapeAnalyzer = new ShapeAnalyzer(globalSettings.SimilarityCoefficient);
            Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
            List<PoreData> list = _processor.BlobsMomentum();

            await Task.Run(() =>
            {
                if (globalSettings.NormalizeBlobArea)
                {
                    var areas = list.Select(item => item.Area);
                    double max = areas.Max();
                    double min = areas.Min();
                    double range = max - min;
                    list.ForEach(data => data.Area = (data.Area - min) / range);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    var bm = list[i];
                    var shapeId = shapeAnalyzer.Analyze(bm.getShapeDescriptor());
                    bm.ShapeId = shapeId;
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


            progressBar.Close();
        }
    }
}
