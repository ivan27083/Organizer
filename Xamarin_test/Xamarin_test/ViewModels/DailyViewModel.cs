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

namespace Xamarin_test.ViewModels
{
    public class DailyViewModel : BaseViewModel
    {
        public Command OpenLoad{ get; }
        public IDataStore<Daily> DataStore => DependencyService.Get<IDataStore<Daily>>();
        public DailyViewModel()
        {
            Title = "Dailies";
            OpenLoad = new Command(async () => await ExecuteLoadItemsCommand());
        }
        private Daily _selectedDaily;
        public ObservableCollection <Daily> Dailys { get; }
        

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
               Dailys.Clear();
                var dailys = await DataStore.GetItemsAsync(true);
                foreach (var daily in dailys)
                {
                    Dailys.Add(daily);
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
        public Daily SelectedDaily
        {
            get => _selectedDaily;
            set
            {
                SetProperty(ref _selectedDaily, value);
                OnAimSelected(value);
            }
        }
        private async void OnAimSelected(Daily daily)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(DailyEditPage)}?{nameof(DailyEditViewModel)}={daily.Id}");
        }
    }
}