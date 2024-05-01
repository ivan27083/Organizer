using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkiaSharp;
using Microcharts;
using Xamarin.Forms;


namespace Xamarin_test.ViewModels
{
    public class EfficiencyViewModel : BaseViewModel
    {
        public EfficiencyViewModel()
        {
            Title = "Efficiency";
        }

        private LineChart lineChart;
        public LineChart LineChart
        {
            get => lineChart;
            set => lineChart = value;
        }

        private void InitData()
        {
            var blueColor = SKColor.Parse("#3498db");
            var chartEntries = new List<ChartEntry>
            {
                new ChartEntry(300)
                {
                    Label = "A",
                    ValueLabel = "300",
                    Color = blueColor
                },
                new ChartEntry(500)
                {
                    Label = "B",
                    ValueLabel = "500",
                    Color = blueColor
                },
                new ChartEntry(700)
                {
                    Label = "C",
                    ValueLabel = "700",
                    Color = blueColor
                },
                new ChartEntry(400)
                {
                    Label = "D",
                    ValueLabel = "400",
                    Color = blueColor
                },
            };

            LineChart = new LineChart { Entries = chartEntries, LabelTextSize = 30f, LabelOrientation = Orientation.Horizontal };
        }
    }
}


