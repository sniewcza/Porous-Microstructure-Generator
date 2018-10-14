﻿using image_processing.Utilities;
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
    public partial class GeneratorProgressBar : Form
    {

        public GeneratorProgressBar()
        {
            InitializeComponent();


        }

        public void Increment()
        {
            if (this.progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() =>
               {
                   progressBar1.Increment(1);
               }
                    ));
            }
        }

        public void setMaxValue(int val)
        {
            if (this.progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Maximum = val;
                }
                    ));
            }
        }

    }
}
