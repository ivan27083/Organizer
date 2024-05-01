using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkiaSharp;
using Microcharts;
using Xamarin.Forms;
using System.Numerics;
using Xamarin.Essentials;


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
        private void InitData_barchart()//ff0000  3498db
        {
            int maxvalue = 1000;
            float v1=300, v2=900, v3=700, v4=500;
            int p1 = (int)Math.Ceiling(v1) * 100 / maxvalue,
                p2 = (int)Math.Ceiling(v2) * 100 / maxvalue,
                p3 = (int)Math.Ceiling(v3) * 100 / maxvalue,
                p4 = (int)Math.Ceiling(v4) * 100 / maxvalue;

            int r0=166, r=255, g0=4, g=192, b0=0, b=64;

            string
                c1 = "#" + Convert.ToString((int)Math.Ceiling(r0 + 0.89 * p1), 16) + Convert.ToString((int)Math.Ceiling(g0 + 1.88 * p1), 16) + Convert.ToString((int)Math.Ceiling(b0 + 0.64 * p1), 16),
                c2 = "#" + Convert.ToString((int)Math.Ceiling(r0 + 0.89 * p2), 16) + Convert.ToString((int)Math.Ceiling(g0 + 1.88 * p2), 16) + Convert.ToString((int)Math.Ceiling(b0 + 0.64 * p1), 16),
                c3 = "#" + Convert.ToString((int)Math.Ceiling(r0 + 0.89 * p3), 16) + Convert.ToString((int)Math.Ceiling(g0 + 1.88 * p3), 16) + Convert.ToString((int)Math.Ceiling(b0 + 0.64 * p1), 16),
                c4 = "#" + Convert.ToString((int)Math.Ceiling(r0 + 0.89 * p4), 16) + Convert.ToString((int)Math.Ceiling(g0 + 1.88 * p4), 16) + Convert.ToString((int)Math.Ceiling(b0 + 0.64 * p1), 16);


            var backgroundColor = SKColor.Parse("#4AB4E4E7"); // Цвет заднего фона
            var blueColor = SKColor.Parse("#FF9040");
            var chartEntries = new List<ChartEntry>
            {
                new ChartEntry(v1)
                {
                    Label = "01.05",
                    ValueLabel = p1.ToString()+"%",
                    Color = SKColor.Parse(c1)
                },
                new ChartEntry(v2)
                {
                    Label = "02.05",
                    ValueLabel = p2.ToString()+"%",
                    Color = SKColor.Parse(c2)
                },
                new ChartEntry(v3)
                {
                    Label = "03.05",
                    ValueLabel = p3.ToString()+"%",
                    Color = SKColor.Parse(c3)
                },
                new ChartEntry(v4)
                {
                    Label = "04.05",
                    ValueLabel = p4.ToString()+"%",
                    Color = SKColor.Parse(c4)
                },
                 new ChartEntry(v4)
                {
                    Label = "04.05",
                    ValueLabel = p4.ToString()+"%",
                    Color = SKColor.Parse(c4)
                },
                  new ChartEntry(v3)
                {
                    Label = "04.05",
                    ValueLabel = p3.ToString()+"%",
                    Color = SKColor.Parse(c3)
                },
                   new ChartEntry(v4)
                {
                    Label = "04.05",
                    ValueLabel = p4.ToString()+"%",
                    Color = SKColor.Parse(c4)
                },
                    new ChartEntry(v2)
                {
                    Label = "04.05",
                    ValueLabel = p2.ToString()+"%",
                    Color = SKColor.Parse(c2)
                },
                     new ChartEntry(v1)
                {
                    Label = "04.05",
                    ValueLabel = p1.ToString()+"%",
                    Color = SKColor.Parse(c1)
                },
            };

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


