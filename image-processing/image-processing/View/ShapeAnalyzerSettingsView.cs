using Generator.Utilities;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Generator.Model;

namespace Generator.View
{
    public partial class ShapeAnalyzerSettingsView : Form
    {
        private string _prevText;
        private Regex _regex = new Regex(@"(0[\.,]{1}\d*)|1|0");
        public ShapeAnalyzerSettingsView(GlobalSettings settings)
        {
            InitializeComponent();
            this.textBox1.TextChanged += TextBox1_TextChanged;
            _prevText = this.textBox1.Text = settings.SimilarityCoefficient.ToString();          
        }

        public double SimilarityCoefficient
        {
            get
            {
                return Convert.ToDouble(this.textBox1.Text.Replace('.',','));
            }
        }
       
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text != string.Empty)
            {
                var match = _regex.Match(this.textBox1.Text);

                if (match.Success)
                {
                    this.textBox1.Text = match.Value;
                    _prevText = match.Value;
                }
                else
                {
                    this.textBox1.Text = _prevText;
                    this.textBox1.ScrollToCaret();
                }
            }
            else
            {
                this._prevText = string.Empty;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
