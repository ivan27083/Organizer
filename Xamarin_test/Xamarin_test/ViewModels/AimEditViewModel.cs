using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_test.Models;
using Xamarin_test.Services;
using Xamarin_test.Views;

namespace Xamarin_test.ViewModels
{
    //[QueryProperty(nameof(_selectedAim), nameof(_selectedAim))]
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class AimEditViewModel : BaseViewModel
    {
        private Purpose _selectedAim;
        private int itemId;
        private string text;
        private string description;
        Purpose item;
        public int Id { get; set; }
        public IDataStore<Purpose> DataStore => DependencyService.Get<IDataStore<Purpose>>();
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public AimEditViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
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

        public Purpose SelectedAim
        {
            get => _selectedAim;
            set
            {
                SetProperty(ref _selectedAim, value);
               
            }
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
                item = await DataStore.GetItemAsync(ItemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        public async void UpdateItem(Purpose aim) // изменение объекта
        {
            try
            {
                var aim1 = await DataStore.UpdateItemAsync(aim);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Update");
            }
        }

        public async void DeleteItem(Purpose aim) //  удаление объекта
        {
            try
            {
                var aim1 = await DataStore.DeleteItemAsync(aim.Id);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Delete");
            }
        }
        private async Task GoBackAsync()
        {
            AimsViewModel.locker = false;
            await Shell.Current.GoToAsync("..");
        }

        private async void OnCancel()
        {
            AimsViewModel.locker = false;
            await Shell.Current.GoToAsync("..");
            // await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={ItemId}");
        }

        private async void OnSave()
        {
            try
            {
                item.Text = Text;
                item.Description = Description;
                var item1 = await DataStore.UpdateItemAsync(item);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Update");
            }
            AimsViewModel.locker = false;
            await Shell.Current.GoToAsync("..");
        }

    }
}
