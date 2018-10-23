using System;
using System.Windows.Forms;

namespace image_processing.View
{
    public partial class FilterBlobsInputBox : Form
    {
        public FilterBlobsInputBox()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(maskedTextBox1.Text != string.Empty && maskedTextBox2.Text != string.Empty )
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
