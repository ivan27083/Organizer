using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_test.ViewModels;

namespace Xamarin_test.Views
{
    public partial class DailyPage : ContentPage
    {
        DailyViewModel _viewModel;
        public DailyPage()
        {
            InitializeComponent();
            BindingContext = _viewModel =  new DailyViewModel();
        }
        public void OnCheckBoxChanged(object sender, CheckedChangedEventArgs e)
        {
            (BindingContext as DailyViewModel).OnCheckBoxChanged(sender, e);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}