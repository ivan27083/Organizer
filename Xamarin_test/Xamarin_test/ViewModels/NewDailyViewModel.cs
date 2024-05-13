using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin_test.Models;
using Xamarin_test.Services;

namespace Xamarin_test.ViewModels
{
    [QueryProperty(nameof(TargetDayId), nameof(TargetDayId))]
    public class NewDailyViewModel : BaseViewModel
    {
        private string text;
        private string description;
        private Day targetDay;
        public IDataStore<Daily> DataStore => DependencyService.Get<IDataStore<Daily>>();
        public MockDataStoreDay DayDataStore => DependencyService.Get<MockDataStoreDay>();
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
                Text = Text,
                Description = Description,
                Day = DateTime.Now.DayOfWeek,
                days = targetDay
            };

            await DataStore.AddItemAsync(newDaily);

            await Shell.Current.GoToAsync("..");
        }

        public int TargetDayId
        {
            get
            {
                return targetDay.Id;
            }
            set
            {
                targetDay = LoadDayId(value);
            }
        }
        public Day LoadDayId(int itemId)
        {
            return DayDataStore.GetItemAsync(itemId).Result;
        }
    }
}
