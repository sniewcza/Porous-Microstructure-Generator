using image_processing.Utilities;
using System;
using System.Windows.Forms;

namespace image_processing.View
{
    public partial class BlobView : Form
    {      
        public BlobView(ShapeAnalyzer shapeAnalyzer,PoreData data)
        {
            InitializeComponent();            
            pictureBox1.Image = data.Bmp;
            listBox1.Items.Add($"M1: {Convert.ToDecimal(data.M1)}");
            listBox1.Items.Add($"M2: {Convert.ToDecimal(data.M2)}");
            listBox1.Items.Add($"M3: {Convert.ToDecimal(data.M3)}");
            listBox1.Items.Add($"M7: {Convert.ToDecimal(data.M7)}");
            listBox1.Items.Add($"M: {Convert.ToDecimal(data.M)}");
            listBox1.Items.Add($"Area: {Convert.ToDecimal(data.Area)}");
            listBox1.Items.Add($"Shape Id: {shapeAnalyzer.Analyze(data.getShapeDescriptor())}");           
        }     
    }
}
