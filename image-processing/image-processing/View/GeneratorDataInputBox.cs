using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace image_processing.View
{
    public partial class GeneratorDataInputBox : Form
    {

        public int MicrostructureWidth { get; set; }
        public int MicrostructureHeight { get; set; }
        public double Volume { get; set; }

        public GeneratorDataInputBox()
        {
            InitializeComponent();           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Volume = double.Parse(textBox1.Text);
                MicrostructureWidth = int.Parse(maskedTextBox1.Text);
                MicrostructureHeight = int.Parse(maskedTextBox2.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Data you provide is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
