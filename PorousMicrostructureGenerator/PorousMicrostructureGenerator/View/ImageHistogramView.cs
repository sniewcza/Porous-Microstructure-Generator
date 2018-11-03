using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System;
using System.Runtime.InteropServices;
using System.Linq;
namespace Generator.View
{
    public partial class ImageHistogramView : Form
    {
        private int _min;
        private int _max;
        public ImageHistogramView(Bitmap image)
        {
            InitializeComponent();
            this.histogram.PositionChanged += Histogram_PositionChanged;
            var values = getHistogramDataFromBmp(image);     
            label1.Text = $"Min: {_min}";
            label2.Text = $"Max: {_max}";
            this.histogram.Values = values;           
        }

        private void Histogram_PositionChanged(object sender, AForge.Controls.HistogramEventArgs e)
        {
            if (e.Position >= 0 && e.Position < histogram.Values.Length)
            {
                label3.Text = $"Level: {e.Position}";
                label4.Text = $"Count: {histogram.Values[e.Position]}";
            }
            else
            {
                label3.Text = "Level:";
                label4.Text = "Count";
            }
        }

        private int[] getHistogramDataFromBmp(Bitmap bitmap)
        {
            var bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            var ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bmpData.Height;
            byte[] colorValues = new byte[bytes];
            Marshal.Copy(ptr, colorValues, 0, bytes);
            bitmap.UnlockBits(bmpData);

            _min = colorValues.Min();
            _max = colorValues.Max();
            int[] values = new int[256];
            foreach (var g in colorValues.GroupBy(b => b))
            {
                values[g.Key] = g.Count();
            }

            return values;
        }
    }
}
