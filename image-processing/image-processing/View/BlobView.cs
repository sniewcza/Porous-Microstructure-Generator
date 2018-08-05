using image_processing.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace image_processing.View
{
    public partial class BlobView : Form
    {
        public BlobView(Bitmap bmp)
        {
            InitializeComponent();
            pictureBox1.Image = bmp;
            BlobMomentum blobMomentum = new BlobMomentum(bmp);

            ImageProcessor imageProcessor = new ImageProcessor();

            var unindexed = imageProcessor.CreateUnindexedBitmap(bmp);

            Graphics g = Graphics.FromImage(unindexed);
            g.DrawEllipse(new Pen(Color.Red), new Rectangle(blobMomentum.CentralX - 1, blobMomentum.CentralY - 1, 2, 2));
            pictureBox1.Image = unindexed;

            listBox1.Items.Add($"M1 {blobMomentum.M1}");
            listBox1.Items.Add($"M2 {blobMomentum.M2}");
            listBox1.Items.Add($"M3 {blobMomentum.M3}");
            listBox1.Items.Add($"M4 {blobMomentum.M4}");
            listBox1.Items.Add($"M5 {blobMomentum.M5}");
            listBox1.Items.Add($"M6 {blobMomentum.M6}");
            listBox1.Items.Add($"M7 {blobMomentum.M7}");
        }
    }
}
