using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using image_processing.Utilities;
using image_processing.View;
using System.Linq;
using System.Threading.Tasks;
using image_processing.Model;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace image_processing
{
    public partial class Form1 : Form
    {
        Utilities.Image _image;
        bool isBinarized;
        IImageProcessor _processor;
        ShapeAnalyzer _shapeAnalyzer;
        GlobalSettings globalSettings;
        GeneratorProgressBar progressBar;
        List<PoreDto> _poresDatabase;

        public Form1(IImageProcessor imageProcessor)
        {
            InitializeComponent();
            _processor = imageProcessor;
            _image = new Utilities.Image();
            _image.OnViewImageChange += _image_OnViewImageChange;
            //  pictureBox1.MouseClick += PictureBox1_MouseClick;
            globalSettings = new GlobalSettings();
            _shapeAnalyzer = new ShapeAnalyzer(globalSettings.SimilarityCoefficient);

            _processor.OnStart += (s, blobsNumber) =>
            {
                progressBar.setMaxValue(blobsNumber * 2);
            };

            _processor.OnProgress += (s, ea) =>
            {
                progressBar.Increment(1);
            };

            pictureBox1.MouseMove += (s, a) =>
             {
                 if (pictureBox1.Image != null)
                 {
                     cordsLabel.Text = $"({a.X},{a.Y}";
                     grayLabel.Text = $"Gray: {_image.ViewImage.GetPixel(a.X, a.Y).R}";
                 }
             };

            pictureBox1.MouseLeave += (s, a) =>
            {
                cordsLabel.Text = string.Empty;
                grayLabel.Text = string.Empty;
            };
            this.SizeChanged += Form1_ResizeEnd;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (_image.ViewImage != null)
            {
                RescalePictureBox();
            }
        }

        //private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        //{
        //    var poreData = _poresDatabase?.FirstOrDefault(data => data.Blob.Rectangle.Contains(e.X, e.Y));
        //    if (poreData != null)
        //    {
        //        BlobView form = new BlobView(_shapeAnalyzer, poreData);
        //        form.Show();
        //    }
        //}

        private void _image_OnViewImageChange(object sender, EventArgs e)
        {
            this.pictureBox1.Image = _image.ViewImage;
            this.volumeLabel.Text = $"Pores volume: {_processor.GetPoresVolume(_image.ViewImage)}% ";
            RescalePictureBox();
            sizeLabel.Text = $"{_image.ViewImage.Width} x {_image.ViewImage.Height}";
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ImageFileDialog.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    var bmp = _processor.ConvertToGrayscale(new Bitmap(ImageFileDialog.FileName));
                    _image.OriginalImage = bmp;
                    _image.ViewImage = bmp;

                    pictureBox1.BorderStyle = BorderStyle.FixedSingle;
                    EnableMenuBarToolstrips();
                    isBinarized = false;
                }
                catch (ArgumentException ex)
                {
                    var extension = ImageFileDialog.FileName.Split('.');
                    MessageBox.Show($"Could not open file with .{extension[extension.Length - 1]} format",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                horizontalPadding = (panelWidth - pictrueWidth) / 2;
            }

            if (pictrueHeight < panelHeight)
            {
                verticalPadding = (panelHeight - pictrueHeight) / 2;
            }

            panel1.Padding = new Padding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
        }

        private void EnableMenuBarToolstrips()
        {
            this.saveToolStripMenuItem.Enabled = true;
            this.reloadToolStripMenuItem.Enabled = true;
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
                form.ThresholdChanged += (o, threshold) =>
                {
                   // int threshold = Convert.ToInt32((textbox as MaskedTextBox).Text);
                    form.PictureBox1.Image = _processor.Binarization(_image.OriginalImage, threshold);
                };
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _image.ViewImage = form.PictureBox1.Image as Bitmap;
                    isBinarized = true;
                }

                form.Dispose();
            }
        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Erosion(_image.ViewImage);
                _image.ViewImage = bmp;
            }
        }

        private void dilatationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Dilatation(_image.ViewImage);
                _image.ViewImage = bmp;
            }
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Opening(_image.ViewImage);
                _image.ViewImage = bmp;
            }
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Closing(_image.ViewImage);
                _image.ViewImage = bmp;
            }
        }

        private void skeletonizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var bmp = _processor.Skeletonization(_image.ViewImage);
                _image.ViewImage = bmp;
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                _image.ViewImage = _image.OriginalImage;
                isBinarized = false;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "bmp";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _image.ViewImage.Save(saveFileDialog.FileName);
            }
        }

        private void sizeDistributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> sizes = _processor.BlobsArea(_image.ViewImage);

            SizeDistributionView distributionView = new SizeDistributionView(sizes);

            distributionView.Show();
        }

        private async void generateMicrostructureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneratorDataInputBox form = new GeneratorDataInputBox();
            if (form.ShowDialog() == DialogResult.OK)
            {
                progressBar = new GeneratorProgressBar
                {
                    Info = "Generating microstructure"
                };

                progressBar.setMaxValue(100);
                DisableMenu();
                DisablePictureBox();

                progressBar.Show();
                //if (_poresData == null)
                //{
                //    _poresData = await Task.Run(() => _processor.FindShapes(_image.ViewImage));
                //    progressBar.Info = "Analyzing shapes";
                //    await AnalyzeShapesAsync(_poresData);
                //}

                //  progressBar.Info = "Generating microstructure";
                MicrostructureGenerator generator = new MicrostructureGenerator(form.MicrostructureWidth, form.MicrostructureHeight);
                generator.OnProgress += (s, percentage) =>
                {
                    // progressBar.setInfo($"Covered area: {percentage}");

                    progressBar.Increment(percentage * 100 / form.Volume);
                };
                var bmp = await Task.Run(() => generator.GenerateMicrostructure(_poresDatabase, form.Volume));
                // var bmp = generator.GenerateMicrostructure(_poresData, form.Volume);
                progressBar.Close();

                EnableMenu();
                EnablePictureBox();

                bmp = _processor.ConvertToGrayscale(bmp);
                _image.ViewImage = bmp;

                RescalePictureBox();
                //  _poresData.Clear();
            }
        }


        private void filterBlobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.ViewImage != null)
            {
                FilterBlobsInputBox form = new FilterBlobsInputBox();
                if (DialogResult.OK == form.ShowDialog())
                {
                    var bmp = _processor.FilterBloobs(_image.ViewImage, form.minWidth, form.minHeight);

                    _image.ViewImage = bmp;

                    form.Dispose();
                }
            }
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShapeAnalyzerSettingsView form = new ShapeAnalyzerSettingsView(globalSettings);

            if (form.ShowDialog() == DialogResult.OK)
            {
                globalSettings.SimilarityCoefficient = form.SimilarityCoefficient;
                globalSettings.NormalizeBlobArea = form.NormalizePoreArea;
            }
        }

        private void DisableMenu()
        {
            menuStrip1.Enabled = false;
        }

        private void DisablePictureBox()
        {
            pictureBox1.Enabled = false;
        }

        private void EnableMenu()
        {
            menuStrip1.Enabled = true;
        }

        private void EnablePictureBox()
        {
            pictureBox1.Enabled = true;
        }

        private async void analyzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isBinarized)
            {
                progressBar = new GeneratorProgressBar
                {
                    Info = "Finding shapes"
                };


                DisableMenu();
                DisablePictureBox();

                progressBar.Show();

                List<PoreData> newData;

                newData = await Task.Run(() => _processor.FindShapes(_image.ViewImage));

                var analyzedShapes = await AnalyzeShapesAsync(newData);

                if (_poresDatabase == null)
                {
                    _poresDatabase = analyzedShapes;
                }
                else
                {
                    _poresDatabase.AddRange(analyzedShapes);
                    _poresDatabase = _poresDatabase.GroupBy(pd => pd.Id).Select(g => g.First()).ToList();
                }
                progressBar.Close();

                EnableMenu();
                EnablePictureBox();
            }
            else
            {
                if (BinarizeFirstDialog() == DialogResult.Yes)
                {
                    binarizationToolStripMenuItem_Click(sender, e);
                    analyzeToolStripMenuItem_Click(sender, e);
                }
            }
        }

        private DialogResult BinarizeFirstDialog()
        {
            return MessageBox.Show("Image should be binarized first. \n Would you like to binarize?",
                    "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        }

        private Task<List<PoreDto>> AnalyzeShapesAsync(List<PoreData> shapes)
        {
            // shapeAnalyzer = new ShapeAnalyzer(globalSettings.SimilarityCoefficient);

            return Task.Run(() =>
           {
               if (globalSettings.NormalizeBlobArea)
               {
                   var areas = shapes.Select(item => item.Area);
                   double max = areas.Max();
                   double min = areas.Min();
                   double range = max - min;
                   shapes.ForEach(data => data.Area = (data.Area - min) / range);
               }

               for (int i = 0; i < shapes.Count; i++)
               {
                   var shape = shapes[i];
                   var shapeId = _shapeAnalyzer.Analyze(shape.getShapeDescriptor());
                   shape.ShapeId = shapeId;
                   progressBar.Increment(1);
               }

               return shapes.GroupBy(s => s.ShapeId)
                .Select(g =>
                {
                    var poredata = g.First();
                    return new PoreDto()
                    {
                        PoreImage = poredata.Bmp,
                        Id = poredata.ShapeId,
                        Area = poredata.Area
                    };
                }).ToList();
           });
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutView form = new AboutView();
            form.ShowDialog();
        }

        private void exportShapesKnowledgeBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PoresKnowledgeBaseDo dataObject = new PoresKnowledgeBaseDo()
                {
                    PoresDictionary = _shapeAnalyzer.ShapeDictionary,
                    PoresData = _poresDatabase
                };


                IFormatter formatter = new BinaryFormatter();
                try
                {
                    Stream stream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                    formatter.Serialize(stream, dataObject);
                    stream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void importShapesKnowledgeBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    PoresKnowledgeBaseDo shapes = (PoresKnowledgeBaseDo)formatter.Deserialize(stream);
                    stream.Close();

                    _shapeAnalyzer.ShapeDictionary = shapes.PoresDictionary;
                    _poresDatabase = shapes.PoresData;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MessageBox.Show("Shape Database loaded", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
