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
            InitData();
        }

        private BarChart barChart;
        public BarChart BarChart
        {
            get => barChart;
            set => SetProperty(ref barChart, value);
        }

        private void InitData()//ff0000  3498db
        {
            float maxvalue = 1000;
            var backgroundColor = SKColor.Parse("#4AB4E4E7"); // Цвет заднего фона
            var blueColor = SKColor.Parse("#FF9040");
            var chartEntries = new List<ChartEntry>
            {
                new ChartEntry(300)
                {
                    Label = "01.05",
                    //ValueLabel = "300",
                    ValueLabel = (300*100/maxvalue).ToString()+" %",
                    Color = blueColor
                },
                new ChartEntry(500)
                {
                    Label = "02.05",
                    ValueLabel = (500*100/maxvalue).ToString()+" %",
                    Color = blueColor
                },
                new ChartEntry(700)
                {
                    Label = "03.05",
                    ValueLabel = (700*100/maxvalue).ToString()+" %",
                    Color = blueColor
                },
                new ChartEntry(400)
                {
                    Label = "04.05",
                    ValueLabel = (400*100/maxvalue).ToString()+" %",
                    Color = blueColor
                },
            };

            BarChart = new BarChart { Entries = chartEntries,
                BackgroundColor = backgroundColor,
                LabelTextSize = 30f,
                MaxValue = maxvalue,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal };
        }
    }
}


