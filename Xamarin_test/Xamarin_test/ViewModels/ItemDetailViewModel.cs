﻿    using System;
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
    public class ItemDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string text;
        private string description;
        public int Id { get; set; }
        public string date;
        public IDataStore<Mission> DataStore => DependencyService.Get<IDataStore<Mission>>();
        public Command deleteItem { get; }
        public Command EditItemCommand { get; }
        public Command GoBack {  get; }
        public ItemDetailViewModel()
        {
            deleteItem = new Command(DeleteItem);
            EditItemCommand = new Command(OnItemSelected);
            GoBack = new Command(GoBackAsync);
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
        public string Date
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
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
                Date = item.Date.ToString("d");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void GoBackAsync(object obj)  // Переход на один уровень вверх в иерархии страниц (Shell)
        {
            AimsViewModel.locker = false;
            await Shell.Current.GoToAsync("..");
        }

        async void OnItemSelected()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemEditPage)}?{nameof(ItemEditViewModel.ItemId)}={ItemId}");
        }
        public async void DeleteItem(object obj) //  удаление объекта
        {
            try
            {
                var item1 = await DataStore.DeleteItemAsync(itemId);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Delete");
            }
        }
    }
}
