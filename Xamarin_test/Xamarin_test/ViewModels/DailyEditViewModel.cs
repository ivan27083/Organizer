using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_test.Models;
using Xamarin_test.Services;
using Xamarin_test.Views;


namespace Xamarin_test.ViewModels
{
    public class DailyEditViewModel : BaseViewModel
    {
        private int dailyId;
        private string text;
        private string description;

        public int Id { get; set; }
        public int day;
        List<Day> days;
        public IDataStore<Daily> DataStore => DependencyService.Get<IDataStore<Daily>>();

        public DailyEditViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description)
                && day>0;
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
        public Day DaysDaily // ?? чет хуйня к
        {
            get => days[dailyId];
            //set => 
        }
        public int DayDaily
        {
            get => day;
            set => SetProperty(ref day, value);
        }

        public int DailyId
        {
            get
            {
                return dailyId;
            }
            set
            {
                dailyId = value;
                LoadDailyId(value);
            }
        }

        public async void LoadDailyId(int dailyId)
        {
            try
            {
                var daily = await DataStore.GetItemAsync(dailyId);
                Id = daily.Id;
                Text = daily.Text;
                Description = daily.Description;
                DayDaily = (int)daily.Day;
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

        public async void DeleteItem(Daily daily) //  удаление объекта
        {
            try
            {
                var daily1 = await DataStore.DeleteItemAsync(daily.Id);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Delete");
            }
        }
        
        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            DayDaily = picker.SelectedIndex+1;
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            
            //string selectedDay = dayPicker.SelectedItem as string;
            //if (!string.IsNullOrEmpty(selectedDay))
            //{
            //    DisplayAlert("Выбранный день", selectedDay, "OK");
            //}
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            try
            {
                Daily changedDaily = new Daily();
                changedDaily.Text = Text;
                changedDaily.Description = Description;
                changedDaily.Day = (DayOfWeek)DayDaily;

                var daily1 = await DataStore.UpdateItemAsync(changedDaily);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Update");
            }
            await Shell.Current.GoToAsync("..");
        }

    }
}
