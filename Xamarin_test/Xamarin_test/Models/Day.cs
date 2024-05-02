using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using Microcharts;
using Xamarin_test.Classes;

namespace Xamarin_test.Models
{
    public class Day
    {
        public DateTime day;
        public List<Daily> dailies;
        public Day()
        {
            day = DateTime.Now;
            dailies = new List<Daily>();
        }
        public ChartEntry ToChartEntry(double maxvalue = 1)
        {
            int completed = 0;
            foreach (Daily d in dailies)
            {
                if (d.Completed) completed++;
            }
            double value = dailies.Count / completed;
            double p1 = (int)Math.Ceiling(value) * 100 / maxvalue;
            ChartColor color = new ChartColor(p1);
            ChartEntry new_entry = new ChartEntry((float)value)
            {
                Label = day.Day.ToString() + "." + day.Month.ToString(),
                ValueLabel = p1.ToString() + "%",
                Color = color.color
            };
            return new_entry;
        }
    }
}
