using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkiaSharp;
using Microcharts;
using Xamarin.Forms;
using System.Numerics;
using Xamarin.Essentials;
using Xamarin_test.Models;


namespace Xamarin_test.ViewModels
{
    public class EfficiencyViewModel : BaseViewModel
    {
        public EfficiencyViewModel()
        {
            Title = "Efficiency";
            InitData_barchart();
        }

        private BarChart barChart;
        public BarChart BarChart
        {
            get => barChart;
            set => SetProperty(ref barChart, value);
        }
        List<Day> days;
        static int maxvalue = 1;
        private void InitData_barchart()//ff0000  3498db
        {
            days = new List<Day>();

            var backgroundColor = SKColor.Parse("#4AB4E4E7"); // Цвет заднего фона
            var blueColor = SKColor.Parse("#FF9040");
            var chartEntries = new List<ChartEntry>();
            foreach (Day day in days)
            {
                chartEntries.Add(day.ToChartEntry());
            }
            //{
            //    new ChartEntry(v1)
            //    {
            //        Label = "01.05",
            //        ValueLabel = p1.ToString()+"%",
            //        Color = SKColor.Parse(c1)
            //    },
            //    new ChartEntry(v2)
            //    {
            //        Label = "02.05",
            //        ValueLabel = p2.ToString()+"%",
            //        Color = SKColor.Parse(c2)
            //    },
            //    new ChartEntry(v3)
            //    {
            //        Label = "03.05",
            //        ValueLabel = p3.ToString()+"%",
            //        Color = SKColor.Parse(c3)
            //    },
            //    new ChartEntry(v4)
            //    {
            //        Label = "04.05",
            //        ValueLabel = p4.ToString()+"%",
            //        Color = SKColor.Parse(c4)
            //    },
            //     new ChartEntry(v4)
            //    {
            //        Label = "04.05",
            //        ValueLabel = p4.ToString()+"%",
            //        Color = SKColor.Parse(c4)
            //    },
            //      new ChartEntry(v3)
            //    {
            //        Label = "04.05",
            //        ValueLabel = p3.ToString()+"%",
            //        Color = SKColor.Parse(c3)
            //    },
            //       new ChartEntry(v4)
            //    {
            //        Label = "04.05",
            //        ValueLabel = p4.ToString()+"%",
            //        Color = SKColor.Parse(c4)
            //    },
            //        new ChartEntry(v2)
            //    {
            //        Label = "04.05",
            //        ValueLabel = p2.ToString()+"%",
            //        Color = SKColor.Parse(c2)
            //    },
            //         new ChartEntry(50)
            //    {
            //        Label = "04.05",
            //        ValueLabel = 5.ToString()+"%",
            //        Color = SKColor.Parse(c1)
            //    },
            //};

            BarChart = new BarChart { Entries = chartEntries,
                BackgroundColor = backgroundColor,
                LabelTextSize = 35f,
                MaxValue = maxvalue,
                //LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal };
        }

        public string Text1_notdone { get; set; } = "Количество невыполненных задач: " + "10";
        public string Text2_done { get; set; } = "Количество выполненных задач: " + "40";
        public string Text3_aims { get; set; } = "Ближайшие задачи: ";
    }
}


