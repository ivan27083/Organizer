using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Xamarin_test.ViewModels
{
    public class DailyViewModel : BaseViewModel
    {
        public ICommand OpenWebCommand { get; }
        public DailyViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }
    }
}