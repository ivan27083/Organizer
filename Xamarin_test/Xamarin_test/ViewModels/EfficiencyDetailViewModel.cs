using System.Linq;
using System.Text;
using SkiaSharp;
using Microcharts;
using Microcharts.Forms;
using Xamarin.Forms;
using System.Numerics;
using Xamarin.Essentials;
using Xamarin_test.Models;
using System.Collections.Generic;
using System;
using Xamarin_test.Services;
using System.Diagnostics;

namespace Xamarin_test.ViewModels
{
    public class EfficiencyDetailViewModel : BaseViewModel
    {
        public IDataStore<Day> DataStore => DependencyService.Get<IDataStore<Day>>();
        Day curr_day;
        private int itemId; 
        public int ItemId
        {
            get
            {
                return curr_day.Id;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                curr_day.Id = item.Id;
                curr_day.day = item.day;
                curr_day.dayOfTheWeek = item.dayOfTheWeek;
                curr_day.dailies = item.dailies;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        public EfficiencyDetailViewModel()
        {
            Title = "Efficiency " + curr_day.day.Date.ToString();
            InitData_piechart();
        }

        private PieChart pieChart;
        public PieChart PieChart
        {
            get => pieChart;
            set => SetProperty(ref pieChart, value);
        }

        static string daily_texts;
        static int completed = 0, notcompleted = 0;

        private void InitData_piechart()
        {
            //////
            DateTime date1 = new DateTime(2024, 5, 3);
            List<Daily> dailies = new List<Daily>()
            {
                new Daily()
            };
            curr_day = new Day(date1, dailies);
            //////



            foreach (Daily d in curr_day.dailies)
            {
                if (d.Completed == true) completed++;
                if (d.Completed == false) notcompleted++;
            }
            completed = 13;
            notcompleted = 4;
            int maxvalue = completed + notcompleted;
            var chartEntries = new List<ChartEntry>
            {
                new ChartEntry(completed)
                {
                    Label = "Completed",
                    ValueLabel = completed.ToString(),
                    Color = SKColor.Parse("#E6FFC040"),
                    ValueLabelColor=SKColor.Parse("#E6FFC040")
                },
                new ChartEntry(notcompleted)
                {
                    Label = "Notcompleted",
                    Color = SKColor.Parse("#B3E83B15"),
                    ValueLabel = notcompleted.ToString(),
                    ValueLabelColor=SKColor.Parse("#B3E83B15") // кристина привет
                },
            };
            //

            PieChart = new PieChart
            {
                Entries = chartEntries,
                BackgroundColor = SKColor.Parse("#4AB4E4E7"),// Цвет заднего фона
                LabelTextSize = 35f,
                MaxValue = maxvalue
            };


            /*foreach (Daily d in curr_day.dailies)
            {
                daily_texts += d.Text + "\n";
            }*/
        }

        public string Text1_stat { get; set; } = "Количество невыполненных задач: " + notcompleted.ToString() + "\nКоличество выполненных задач: " + completed.ToString();
        public string Text3_aims { get; set; } = "Все задачи дня: " + daily_texts;
    }
}
