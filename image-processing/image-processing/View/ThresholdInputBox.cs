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
    public partial class ThresholdInputBox : Form
    {
        public event EventHandler ThresholdChanged;
        public ThresholdInputBox(Bitmap bitmap)
        {
            InitializeComponent();
            maskedTextBox1.TextChanged += MaskedTextBox1_TextChanged;
            pictureBox1.Image = bitmap;
           
        }

        private void MaskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            int threshold;
            var validFormat = Int32.TryParse(maskedTextBox1.Text, out threshold);
            if (validFormat && threshold >= 0 && threshold <= 255)
                ThresholdChanged(sender, e);
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
