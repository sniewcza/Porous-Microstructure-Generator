using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace image_processing.View
{
    public partial class SizeDistributionView : Form
    {

        List<string> labels = new List<string>();
        private List<int> _blobSizes;

        public SizeDistributionView(List<int> sizes)
        {
            InitializeComponent();
            _blobSizes = sizes;
            chart1.Customize += Chart1_Customize;
            GenerateChartData();

        }

        private void GenerateChartData()
        {
            //Ranges
            List<int> ranges = new List<int>();
            int range = 100;
            while (range < _blobSizes.Last())
            {
                ranges.Add(range);
                range += 100;
            }
            //GroupData
            var seriesData = _blobSizes.GroupBy(x => ranges.FirstOrDefault(r => r >= x))
                        .Select(g => new { Value = g.Key, Count = g.Count() })
                        .OrderByDescending(x => x.Value).Where(x => x.Value != 0).Reverse();
            //Labels
            labels.Add("1\n100");
            for (int i = 0; i < seriesData.Count() - 1; i++)
            {
                labels.Add($"{seriesData.ElementAt(i).Value + 1}\n{seriesData.ElementAt(i + 1).Value}");
            }
            //Series
            foreach (var p in seriesData)
            {
                chart1.Series["Series1"].Points.AddXY(p.Value, p.Count);
            }
        }

        private void Chart1_Customize(object sender, EventArgs e)
        {
            int index = 0;
            foreach (var p in chart1.Series["Series1"].Points)
            {
                p.AxisLabel = labels[index];
                index++;
            }
        }


    }
}
