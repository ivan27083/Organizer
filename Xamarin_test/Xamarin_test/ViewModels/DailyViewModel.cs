using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin_test.Models;
using Xamarin_test.Views;
using Xamarin_test.Services;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

namespace Xamarin_test.ViewModels
{
    public class DailyViewModel : BaseViewModel
    {
        public IDataStore<Daily> DataStore => DependencyService.Get<IDataStore<Daily>>();
        public MockDataStoreDay DayDataStore => DependencyService.Get<MockDataStoreDay>();
        public ObservableCollection<Daily> Dailies { get; }
        public Command LoadItemsCommand{ get; }
        public Command AddItemCommand { get; }
        public Command<Daily> ItemTapped { get; }
        public Day TargetDay;
        public DailyViewModel()
        {
            TargetDay = GetDay();

            Title = "Dailies";
            Dailies = new ObservableCollection<Daily>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            _ = ExecuteLoadItemsCommand();
            ItemTapped = new Command<Daily>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }

        private Day GetDay()
        {
            DateTime dateTime = DateTime.Now;
            var day = DayDataStore.GetItemByDayAsync(dateTime.Date).Result;

            if (day != null)
            {
                return day;
            }

            day = new Day();
            var id = DayDataStore.AddItemAsync(day);
            id.Wait();
            day.Id = id.Result;
            return day;
        }

        public void OnCheckBoxChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var daily = (Daily)checkBox.BindingContext;
            if (daily != null)
            {
                daily.Completed = e.Value;
                DataStore.UpdateItemAsync(daily);
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
               Dailies.Clear();
                var dailys = await DataStore.GetItemsAsync(true);
                TargetDay = GetDay();
                foreach (var daily in TargetDay.dailies)
                {
                    Dailies.Add(daily);
                }
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
        
        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewDailyPage)}?{nameof(NewDailyViewModel.TargetDayId)}={TargetDay.Id}");
        }

        async void OnItemSelected(Daily daily)
        {
            if (daily == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack

            await Shell.Current.GoToAsync($"{nameof(DailyDetailPage)}?{nameof(DailyDetailViewModel.ItemId)}={daily.Id}");
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}