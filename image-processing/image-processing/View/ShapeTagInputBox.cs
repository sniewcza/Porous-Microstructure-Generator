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
    public partial class ShapeTagInputBox : Form
    {
        public string ShapeTag { get; set; }
        public ShapeTagInputBox()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShapeTag = textBox1.Text.Trim();
            if (ShapeTag != String.Empty) 
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
