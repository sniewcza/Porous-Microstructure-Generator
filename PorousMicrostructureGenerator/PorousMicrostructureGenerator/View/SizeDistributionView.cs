using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Generator.View
{
    public partial class SizeDistributionView : Form
    {

        private int _minPoreArea;
        private int _maxPoreArea;
        private IGrouping<int, int>[] grouping;
        public SizeDistributionView(List<int> sizes)
        {
            InitializeComponent();
            grouping = InitializeHistogramData(sizes);
            this.histogram.Values = grouping.Select(g => g.Count()).ToArray();
            this.histogram.Width = this.histogram.Values.Length+10;
            this.histogram.PositionChanged += Histogram_PositionChanged;
            this.label1.Text = $"Min area: {_minPoreArea}";
            this.label2.Text = $"Max area: {_maxPoreArea}";
            this.histogram.Refresh();
        }

        private void Histogram_PositionChanged(object sender, AForge.Controls.HistogramEventArgs e)
        {
            if (e.Position >= 0 && e.Position < histogram.Values.Length)
            {
                label3.Text = $"Area: {grouping[e.Position].Key}";
                label4.Text = $"Count: {histogram.Values[e.Position]}";
            }
            else
            {
                label3.Text = "Area:";
                label4.Text = "Count";
            }
        }

        private IGrouping<int, int>[] InitializeHistogramData(List<int> sizes)
        {
            var groups = sizes.GroupBy(s => s);

            _minPoreArea = groups.Min(g => g.Key);
            _maxPoreArea = groups.Max(g => g.Key);

            //  List<int> histogramData = new List<int>
            // int[] histogramValues = new int[_maxPoreArea+1];

            //foreach (var group in groups)
            //{
            //    histogramValues[group.Key] = group.Count();
            //}

            return groups.OrderBy(g => g.Key).ToArray(); ;
        }


    }
}
