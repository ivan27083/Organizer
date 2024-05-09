using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Xamarin_test.Models;
using Xamarin_test.Services;
using Xamarin_test.Views;

namespace Xamarin_test.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Mission _selectedMission;
        public IDataStore<Mission> DataStore => DependencyService.Get<IDataStore<Mission>>();
        public ObservableCollection<Mission> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Mission> ItemTapped { get; }
        public ItemsViewModel()
        {
            Title = "Tasks";
            Items = new ObservableCollection<Mission>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            _ = ExecuteLoadItemsCommand();
            ItemTapped = new Command<Mission>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }
        
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var missions = await DataStore.GetItemsAsync(true);
                foreach (var mission in missions)
                {
                    Items.Add(mission);
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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Mission SelectedItem
        {
            get => _selectedMission;
            set
            {
                SetProperty(ref _selectedMission, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Mission mission)
        {
            if (mission == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={mission.Id}");
        }
    }
}