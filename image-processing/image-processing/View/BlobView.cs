using AForge.Imaging;
using image_processing.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace image_processing.View
{
    public partial class BlobView : Form
    {
        private ShapeAnalyzer _shapeAnalyzer;
        PoorData pordata;
        public BlobView(Blob blob,ShapeAnalyzer shapeAnalyzer,PoorData data)
        {
            InitializeComponent();

            _shapeAnalyzer = shapeAnalyzer;
            pordata = data;
                      
            pictureBox1.Image = pordata.Bmp;

            listBox1.Items.Add($"M1: {Convert.ToDecimal(pordata.M1)}");
            listBox1.Items.Add($"M2: {Convert.ToDecimal(pordata.M2)}");
            listBox1.Items.Add($"M3: {Convert.ToDecimal(pordata.M3)}");          
            listBox1.Items.Add($"M7: {Convert.ToDecimal(pordata.M7)}");         
            listBox1.Items.Add($"M: {Convert.ToDecimal(pordata.M)}");
            listBox1.Items.Add($"Area: {Convert.ToDecimal(pordata.Area)}");
            listBox1.Items.Add($"Shape Id: {_shapeAnalyzer.Analyze(pordata.getShapeDescriptor())}");
           
        }

       
    }
}
