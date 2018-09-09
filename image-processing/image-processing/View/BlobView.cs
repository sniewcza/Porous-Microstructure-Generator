using AForge.Imaging;
using image_processing.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace image_processing.View
{
    public partial class BlobView : Form
    {
        public BlobView(Blob blob)
        {
            InitializeComponent();
            
           
            ShapeAnalyzer shapeAnalyzer = new ShapeAnalyzer();
            ImageProcessor imageProcessor = new ImageProcessor();
            var reversed = blob.Image.ToManagedImage();
            BlobCounter blobCounter = new BlobCounter(reversed);                     
            Blob[] blobs = blobCounter.GetObjects(reversed, false);
            var edgepoints = blobCounter.GetBlobsEdgePoints(blobs[0]);
            
            BlobMomentum blobMomentum = new BlobMomentum(imageProcessor.ReverseBitmapColors( blobs[0].Image.ToManagedImage()),edgepoints,blobs[0].Area);
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
            listBox1.Items.Add(shapeAnalyzer.Analyze(blobMomentum.getShapeDescriptor()));
        }
    }
}
