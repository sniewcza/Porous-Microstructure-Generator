using System;
using System.Threading;
using System.Windows.Forms;

namespace image_processing.View
{


    public partial class GeneratorProgressBar : Form
    {
        private double _progress;
        public CancellationTokenSource cancellationToken;

        public GeneratorProgressBar()
        {
            InitializeComponent();
        }

        public void Increment(double val)
        {
            _progress += val;
            if (_progress >= 1)
            {
                _progress -= 1;
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Increment(1);
                }));
            }
        }

        public void setInfo(string info)
        {
            if (label1.InvokeRequired)
            {
                label1.BeginInvoke(new Action(() => label1.Text = info));
            }
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

        private void GeneratorProgressBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }
    }
}
