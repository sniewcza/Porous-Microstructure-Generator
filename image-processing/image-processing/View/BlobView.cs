using AForge.Imaging;
using image_processing.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace image_processing.View
{
    public partial class BlobView : Form
    {
        private ShapeAnalyzer _shapeAnalyzer;
        BlobMomentum blobMomentum;
        public BlobView(Blob blob,ShapeAnalyzer shapeAnalyzer)
        {
            InitializeComponent();

            _shapeAnalyzer = shapeAnalyzer;
          
            ImageProcessor imageProcessor = new ImageProcessor();
            var reversed = blob.Image.ToManagedImage();
            BlobCounter blobCounter = new BlobCounter(reversed);                     
            Blob[] blobs = blobCounter.GetObjects(reversed, false);
            var edgepoints = blobCounter.GetBlobsEdgePoints(blobs[0]);                    
           blobMomentum = new BlobMomentum(imageProcessor.ReverseBitmapColors( blobs[0].Image.ToManagedImage()),edgepoints,blobs[0].Area);
            // var unindexed = imageProcessor.ReverseBitmapColors(blobMomentum.Bmp);

            //  Graphics g = Graphics.FromImage(unindexed);
            // g.DrawEllipse(new Pen(Color.Red), new Rectangle(blobMomentum.CentralX - 1, blobMomentum.CentralY - 1, 2, 2));
            pictureBox1.Image = blobMomentum.Bmp;

            listBox1.Items.Add($"M1 {Convert.ToDecimal(blobMomentum.M1)}");
            listBox1.Items.Add($"M2 {Convert.ToDecimal(blobMomentum.M2)}");
            listBox1.Items.Add($"M3 {Convert.ToDecimal(blobMomentum.M3)}");          
            listBox1.Items.Add($"M7 {Convert.ToDecimal(blobMomentum.M7)}");
            listBox1.Items.Add($"Lp1 {Convert.ToDecimal(blobMomentum.Lp1)}");
            listBox1.Items.Add($"M {Convert.ToDecimal(blobMomentum.M)}");
            listBox1.Items.Add(_shapeAnalyzer.Analyze(blobMomentum.getShapeDescriptor()));
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var inputBox = new ShapeTagInputBox())
            {
                inputBox.ShowDialog();
                if(inputBox.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        _shapeAnalyzer.AddTrainingData(blobMomentum.getShapeDescriptor(), inputBox.ShapeTag);
                    }
                    catch(ArgumentException ex)
                    {
                        MessageBox.Show("This shape already exists in training set");
                    }
                }
            }
        }
    }
}
