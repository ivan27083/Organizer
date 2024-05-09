using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_test.Models;
using Xamarin_test.Services;
using Xamarin_test.Views;

namespace Xamarin_test.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    
    public class ItemEditViewModel : BaseViewModel
    {
        private int itemId;
        private string text;
        private string description;
        Mission item;
        public int Id { get; set; }
        DateTime date;
        public DateTime MinimumDate;
        public IDataStore<Mission> DataStore => DependencyService.Get<IDataStore<Mission>>();
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ItemEditViewModel()
        {
            MinimumDate = DateTime.Now;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description)
                && date > MinimumDate;
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public int ItemId
        {
            get
            {
                return itemId;
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
                item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
                Date = item.Date;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async Task GoBackAsync()  // Переход на один уровень вверх в иерархии страниц (Shell)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={ItemId}");
        }

        private async void OnSave()
        {
            try
            {
                item.Text = Text;
                item.Description = Description;
                item.Date = Date;
                var item1 = await DataStore.UpdateItemAsync(item);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Update");
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
