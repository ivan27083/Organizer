using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin_test.Models;
using Xamarin_test.Services;

namespace Xamarin_test.ViewModels
{
    public class NewAimViewModel : BaseViewModel
    {
        private string text;
        private string description;
        private int id;
        public int group;
        public IDataStore<Purpose> DataStore => DependencyService.Get<IDataStore<Purpose>>();
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }
        public int Id
        {
            get => id;
            set=> SetProperty(ref id, value);
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        public NewAimViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private async void OnSave()
        {
            Purpose newAim = new Purpose();
            {
                //Id = Id, // id errors mb ???
                //Text = Text,
                //Description = Description
            };

            await DataStore.AddItemAsync(newAim);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }



    }
}
