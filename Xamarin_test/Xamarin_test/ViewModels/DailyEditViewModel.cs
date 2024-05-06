﻿using System;
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
        int day;
        List<Day> days;
        public IDataStore<Daily> DataStore => DependencyService.Get<IDataStore<Daily>>();
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

        public async void UpdateItem(Daily daily) // изменение объекта
        {
            try
            {
                var daily1 = await DataStore.UpdateItemAsync(daily);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Update");
            }
        }

        public async void DeleteItem(Daily daily) //  удаление объекта
        {
            try
            {
                var daily1 = await DataStore.DeleteItemAsync(daily.Id);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Delete");
            }
        }
    }
}