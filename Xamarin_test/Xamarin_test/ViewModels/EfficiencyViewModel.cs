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
            DateTime date1 = new DateTime(2024, 5, 1);
            List<Daily> dailies = new List<Daily>()
            {
                new Daily()
            };
            days = new List<Day>()
            {
                new Day(date1, dailies)
            };
            var blueColor = SKColor.Parse("#FF9040");//?????
            var chartEntries = new List<ChartEntry>();
            foreach (Day day in days)
            {
                chartEntries.Add(day.ToChartEntry());
            }

            BarChart = new BarChart { Entries = chartEntries,
                BackgroundColor = SKColor.Parse("#4AB4E4E7"),// Цвет заднего фона
                LabelTextSize = 35f,
                MaxValue = maxvalue,
                ValueLabelOrientation = Orientation.Horizontal };
            if (days.Count < 4) BarChart.LabelOrientation = Orientation.Horizontal;
        }

        public string Text1_notdone { get; set; } = "Количество невыполненных задач: " + "10";
        public string Text2_done { get; set; } = "Количество выполненных задач: " + "40";
        public string Text3_aims { get; set; } = "Ближайшие задачи: ";
    }
}


