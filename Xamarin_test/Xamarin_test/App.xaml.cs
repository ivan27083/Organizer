using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_test.Services;
using Xamarin_test.Views;
using Xamarin_test.Classes;
using System.IO;

using System.Linq;

namespace Xamarin_test
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "base.db";
        public App()
        {
            InitializeComponent();
            string dbPath = DependencyService.Get<IPath>().GetDatabasePath(DATABASE_NAME);
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockDataStoreDaily>();
            DependencyService.Register<MockDataStoreDay>();
            DependencyService.Register<MockDataStorePurpose>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
