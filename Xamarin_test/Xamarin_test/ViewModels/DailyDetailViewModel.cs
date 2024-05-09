using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Xamarin_test.Models;
using Xamarin_test.Services;

namespace Xamarin_test.ViewModels
{
    public class DailyDetailViewModel : BaseViewModel
    {
        public IDataStore<Daily> DataStore => DependencyService.Get<IDataStore<Daily>>();
        private int itemId;
        private string text;
        private string description;
        public int Id { get; set; }

        public Command deleteItem { get; }
        public DailyDetailViewModel()
        {
            deleteItem = new Command(DeleteItem);
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
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
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
