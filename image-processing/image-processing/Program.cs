﻿using image_processing.Utilities;
using System;
using System.Windows.Forms;

namespace image_processing
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(new ImageProcessor()));
        }
    }
}
