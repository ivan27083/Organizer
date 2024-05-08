using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_test.ViewModels;

namespace Xamarin_test.Views
{
    public partial class DailyPage : ContentPage
    {
        public DailyPage()
        {
            InitializeComponent();
            BindingContext = new DailyViewModel();
        }
    }
}