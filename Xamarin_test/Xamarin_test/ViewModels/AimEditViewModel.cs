using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Xamarin_test.Models;
using Xamarin_test.Services;
using Xamarin_test.Views;

namespace Xamarin_test.ViewModels
{
    [QueryProperty(nameof(_selectedAim), nameof(_selectedAim))]

    public class AimEditViewModel : BaseViewModel
    {
        private Purpose _selectedAim;
        public IDataStore<Purpose> DataStore => DependencyService.Get<IDataStore<Purpose>>();

        public AimEditViewModel()
        {
          
        }
        public Purpose SelectedAim
        {
            get => _selectedAim;
            set
            {
                SetProperty(ref _selectedAim, value);
               
            }
        }
        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(_selectedAim.Id);
                //Id = item.Id;
                //Text = item.Text;
                //Description = item.Description;
                //Date = item.Date;
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

    }
}
