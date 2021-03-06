﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Generator.Utilities;
using Generator.View;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Generator.Model;

namespace Generator
{
    public partial class Form1 : Form
    {
        Model.Image _image;
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
            _image = new Model.Image();
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
                     try
                     {
                         cordsLabel.Text = $"({a.X},{a.Y})";
                         grayLabel.Text = $"Gray: {_image.ViewImage.GetPixel(a.X, a.Y).R}";
                     }
                     catch (IndexOutOfRangeException ex) { }
                 }
             };

            pictureBox1.MouseLeave += (s, a) =>
            {
                cordsLabel.Text = string.Empty;
                grayLabel.Text = string.Empty;
            };

            this.SizeChanged += Form1_ResizeEnd;

            _shapeAnalyzer.ShapeCountChange += (s, count) =>
            {
                setShapeCountLabel(count);
            };
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
            this.analyzeToolStripMenuItem.Enabled = true;
        }

        private void binarizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.OriginalImage != null)
            {
                var form = new ThresholdInputBox(_image.OriginalImage);
                form.ThresholdChanged += (o, threshold) =>
                {
                   
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
            if (_image.ViewImage != null)
            {
                List<int> sizes = _processor.BlobsArea(_image.ViewImage);
                SizeDistributionView distributionView = new SizeDistributionView(sizes);
                distributionView.Show();
            }
        }

        private async void generateMicrostructureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_shapeAnalyzer.ShapeDictionary.Count == 0)
            {
                string msg = "Import shape database or analyze image first!";
                string caption = "Shape database is empty";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
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

                    //int physicalWidth = Convert.ToInt32(form.MicrostructureWidth * form.Ratio);
                    //int physicalHeight = Convert.ToInt32(form.MicrostructureHeight * form.Ratio);

                    MicrostructureGenerator generator = new MicrostructureGenerator(form.MicrostructureWidth, form.MicrostructureHeight, 1/form.Ratio, _processor);
                    generator.OnProgress += (s, percentage) =>
                    {


                        progressBar.Increment(percentage * 100 / form.Volume);
                    };
                    var bmp = await Task.Run(() => generator.GenerateMicrostructure(_poresDatabase, form.Volume));

                    progressBar.Dispose();

                    EnableMenu();
                    EnablePictureBox();

                    int pixelWidth = Convert.ToInt32(form.MicrostructureWidth * 1 / form.Ratio);
                    int pixelHeight = Convert.ToInt32(form.MicrostructureHeight * 1 / form.Ratio);
                     bmp = _processor.Rescale(bmp,pixelWidth,pixelHeight); 
                    var bm2 = _processor.ConvertToGrayscale(bmp);
                    _image.ViewImage = _processor.Binarization(bm2,127);
                    RescalePictureBox();
                }
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


                _shapeAnalyzer = new ShapeAnalyzer(_shapeAnalyzer, globalSettings.SimilarityCoefficient);

                _shapeAnalyzer.ShapeCountChange += (s, count) =>
                {
                    setShapeCountLabel(count);
                };
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
                    Info = "Looking for shapes",
                };


                DisableMenu();
                DisablePictureBox();

                progressBar.Show();

                List<PoreAnalyzeData> newData;

                newData = await Task.Run(() => _processor.FindShapes(_image.ViewImage));

                progressBar.Info = "Analyzing shapes";

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
                progressBar.Dispose();

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

        private void setShapeCountLabel(int count)
        {
            shapeCountLabel.BeginInvoke(new Action(() =>
            {
                shapeCountLabel.Text = $"Shapes in store: {count}";
            }));
        }
        private DialogResult BinarizeFirstDialog()
        {
            return MessageBox.Show("Image should be binarized first. \n Would you like to binarize?",
                    "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        }

        private Task<List<PoreDto>> AnalyzeShapesAsync(List<PoreAnalyzeData> shapes)
        {
            return Task.Run(() =>
           {

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

                    MessageBox.Show("Shape Database loaded", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void imageHistogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image.ViewImage != null)
            {
                ImageHistogramView form = new ImageHistogramView(_image.ViewImage);
                form.Show();
            }
        }

        private void clearShapeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _shapeAnalyzer.ShapeDictionary.Clear();
            setShapeCountLabel(_shapeAnalyzer.ShapeDictionary.Count);
        }
    }
}
