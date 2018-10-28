using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Controls;
namespace image_processing.View
{
    public partial class ThresholdInputBox : Form
    {
        public event EventHandler<int> ThresholdChanged;     
        public ThresholdInputBox(Bitmap bitmap)
        {
            InitializeComponent();
            maskedTextBox1.TextChanged += MaskedTextBox1_TextChanged;
            slider.ValuesChanged += Slider_ValuesChanged;
            pictureBox1.Image = bitmap;         
        }

        private void Slider_ValuesChanged(object sender, EventArgs e)
        {
            maskedTextBox1.Text = slider.Min.ToString();
            MaskedTextBox1_TextChanged(sender, e);
        }

        private void MaskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            int threshold;
            var validFormat = Int32.TryParse(maskedTextBox1.Text, out threshold);
            if (validFormat && threshold >= 0 && threshold <= 255)
            {
                slider.Min = threshold;
                ThresholdChanged(sender, threshold);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
