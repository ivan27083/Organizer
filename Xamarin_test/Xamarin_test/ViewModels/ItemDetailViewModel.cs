﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_test.Models;
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

        private async Task GoBackAsync()  // Переход на один уровень вверх в иерархии страниц (Shell)
        {
            await Shell.Current.GoToAsync("..");
        }

        async void OnItemSelected()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemEditPage)}?{nameof(ItemEditViewModel)}={itemId}");
        }
    }
}
