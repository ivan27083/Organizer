using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin_test.ViewModels;
using Xamarin_test.Views;

namespace Xamarin_test
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(DailyDetailPage), typeof(DailyDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(NewDailyPage), typeof(NewDailyPage));
            Routing.RegisterRoute(nameof(ItemEditPage), typeof(ItemEditPage));
            Routing.RegisterRoute(nameof(DailyEditPage), typeof(DailyEditPage));
            Routing.RegisterRoute(nameof(AimEditPage), typeof(AimEditPage));
            Routing.RegisterRoute(nameof(NewAimPage), typeof(NewAimPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
