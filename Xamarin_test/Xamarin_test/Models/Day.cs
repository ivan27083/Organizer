using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using Microcharts;
using Xamarin_test.Classes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin_test.Models
{
    public class Day
    {
        public int Id { get; set; }
        public DateTime day;
        [NotMapped]public List<Daily> dailies;
        public DayOfWeek? dayOfTheWeek;
        public Daily? daily_nav {  get; set; }
        public Day()
        {
            day = DateTime.Now;
            dailies = new List<Daily>();
            dayOfTheWeek = day.DayOfWeek;
        }
        public Day(DateTime _day, List<Daily> _dailies)
        {
            day = _day;
            dailies = _dailies;
            dayOfTheWeek = day.DayOfWeek;
        }
        public ChartEntry ToChartEntry(double maxvalue = 1)
        {
            int completed = 0;
            foreach (Daily d in dailies)
            {
                if (d.Completed) completed++;
            }
            double value = completed > 0 ? dailies.Count / completed : 0;
            double p1 = (int)Math.Ceiling(value) * 100 / maxvalue;
            ChartColor color = new ChartColor(p1);
            ChartEntry new_entry = new ChartEntry((float)value)
            {
                Label = (day.Day > 10 ? day.Day.ToString() : "0" + day.Day.ToString()) + "." + (day.Month > 10 ? day.Month.ToString(): "0" + day.Month.ToString()),
                ValueLabel = p1.ToString() + "%",
                Color = color.color
            };
            return new_entry;
        }
    }
}
