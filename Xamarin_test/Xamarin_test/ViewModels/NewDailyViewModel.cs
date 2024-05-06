using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin_test.Models;
using Xamarin_test.Services;

namespace Xamarin_test.ViewModels
{
    public class NewDailyViewModel : BaseViewModel
    {
        private string text;
        private string description;
        public IDataStore<Daily> DataStore => DependencyService.Get<IDataStore<Daily>>();
        
        public NewDailyViewModel()
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Daily newDaily = new Daily()
            {
                Id = 0, // id errors mb
                Text = Text,
                Description = Description
            };

            await DataStore.AddItemAsync(newDaily);

            await Shell.Current.GoToAsync("..");
        }
    }
}
