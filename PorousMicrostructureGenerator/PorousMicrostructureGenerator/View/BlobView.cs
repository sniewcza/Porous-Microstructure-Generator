using Generator.Utilities;
using System;
using System.Windows.Forms;
using Generator.Model;

namespace Generator.View
{
    public partial class BlobView : Form
    {      
        public BlobView(ShapeAnalyzer shapeAnalyzer,PoreAnalyzeData data)
        {
            InitializeComponent();            
            pictureBox1.Image = data.Bmp;
            listBox1.Items.Add($"Moment invariant 1: {Convert.ToDecimal(data.GeometricMoment1)}");
            listBox1.Items.Add($"Moment invariant 2: {Convert.ToDecimal(data.GeometricMoment2)}");
            listBox1.Items.Add($"Moment invariant 3: {Convert.ToDecimal(data.GeometricMoment3)}");
            listBox1.Items.Add($"Moment invariant 7: {Convert.ToDecimal(data.GeometricMoment7)}");
            listBox1.Items.Add($"Malinowska's coefficient: {Convert.ToDecimal(data.MalinowskasCoefficient)}");
            listBox1.Items.Add($"Area: {Convert.ToDecimal(data.Area)}");
            listBox1.Items.Add($"Shape Id: {shapeAnalyzer.Analyze(data.getShapeDescriptor())}");           
        }     
    }
}
