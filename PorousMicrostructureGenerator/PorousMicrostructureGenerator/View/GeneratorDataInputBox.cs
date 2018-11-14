using System;
using System.Windows.Forms;

namespace Generator.View
{
    public partial class GeneratorDataInputBox : Form
    {

        public int MicrostructureWidth { get; set; }
        public int MicrostructureHeight { get; set; }
        public double Volume { get; set; }
        public double Ratio { get; set; }

        public GeneratorDataInputBox()
        {
            InitializeComponent();           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool dataOk = false;
            try
            {
                Volume = double.Parse(textBox1.Text);
                MicrostructureWidth = int.Parse(maskedTextBox1.Text);
                MicrostructureHeight = int.Parse(maskedTextBox2.Text);
                Ratio = double.Parse(textBox2.Text);
                dataOk = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Data you provide is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (dataOk)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

       
    }
}
