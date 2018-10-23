using System;
using System.Windows.Forms;

namespace image_processing.View
{
    public partial class GeneratorProgressBar : Form
    {
        public GeneratorProgressBar()
        {
            InitializeComponent();
        }

        public void Increment()
        {
            progressBar1.BeginInvoke(new Action(() =>
            {
               progressBar1.Increment(1);
            }));
        }

        public void setMaxValue(int val)
        {
            if (this.progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Maximum = val;
                }));
            }
        }
    }
}
