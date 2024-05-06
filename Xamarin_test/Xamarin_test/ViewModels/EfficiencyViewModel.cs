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
using Xamarin_test.Services;
using Xamarin_test.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Xamarin_test.ViewModels
{
    public class EfficiencyViewModel : BaseViewModel
    {
        public IDataStore<Day> DataStore => DependencyService.Get<IDataStore<Day>>();
        public Command LoadDaysCommand { get; }
        public Command ButtonEfficiency2 { get; }
        public ObservableCollection<Day> Days { get; }
        
        public EfficiencyViewModel()
        {
            Days = new ObservableCollection<Day>();
            LoadDaysCommand = new Command(async () => await ExecuteLoadDaysCommand());

            Title = "Efficiency";

            ButtonEfficiency2 = new Command(OnEfficiency2);
        }

        async Task ExecuteLoadDaysCommand()
        {
            IsBusy = true;

            try
            {
                Days.Clear();
                var days = await DataStore.GetItemsAsync(true);
                foreach (var d in days)
                {
                    Days.Add(d);
                }
                InitData_barchart();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnEfficiency2()
        {
           await Shell.Current.GoToAsync(nameof(EfficiencyDetailPage));
        }

        /*async void OnBarSelected(ChartEntry bar)
        {
            if (bar == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            var day = days.Find(u => u.day == DateTime.ParseExact(bar.Label, "dd.MM", System.Globalization.CultureInfo.InvariantCulture));
            await Shell.Current.GoToAsync($"{nameof(EfficiencyDetailPage)}?{nameof(EfficiencyDetailViewModel.ItemId)}={day.Id}");
        }

        private async void callback(object sender, EventArgs e)
        {
            var chart = sender as BarChart;
            OnBarSelected(chart.Entries.FirstOrDefault());
        }
        

        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var chart = sender as Microcharts.Forms.ChartView;
            
            if (chart.Chart is BarChart barChart)
            {
                chart.GestureRecognizers.Add(tapGestureRecognizer);
                tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                var tapPosition = e as TappedEventArgs;


                //var columnIndex = (int)(tapPosition.Parameter / columnWidth);

                var columnWidth = chart.Width / barChart.Entries.Count();
                
                //var position = e.GetPosition(chart);
                //var columnIndex = (int)(position.X / columnWidth);

                //if (columnIndex >= 0 && columnIndex < barChart.Entries.Count())
                //{
                //    var tappedEntry = barChart.Entries.ElementAt(columnIndex);
                //    // tappedEntry для выполнения необходимых действий
                //}
            }
        }*/

        private BarChart barChart;
        public BarChart BarChart
        {
            get => barChart;
            set => SetProperty(ref barChart, value);
        }
        //List<Day> days;
        static int maxvalue = 1;


        
        private void InitData_barchart()
        {
            /*DateTime date1 = new DateTime(2024, 5, 1);
            List<Daily> dailies = new List<Daily>()
            {
                new Daily()
            };
            days = new List<Day>()
            {
                new Day(date1, dailies)
            };*/

            var chartEntries = new List<ChartEntry>();
            foreach (Day day in Days)
            {
                chartEntries.Add(day.ToChartEntry());
            }
            
            
            BarChart = new BarChart
            {
                Entries = chartEntries,
                BackgroundColor = SKColor.Parse("#4AB4E4E7"),// Цвет заднего фона
                LabelTextSize = 35f,
                MaxValue = maxvalue,
                ValueLabelOrientation = Orientation.Horizontal
            };
            
            if (Days.Count < 4) BarChart.LabelOrientation = Orientation.Horizontal;
        }

        public string Text1_notdone { get; set; } = "Количество невыполненных задач: " + "10";
        public string Text2_done { get; set; } = "Количество выполненных задач: " + "40";
        public string Text3_aims { get; set; } = "Ближайшие задачи: ";
    }
}


