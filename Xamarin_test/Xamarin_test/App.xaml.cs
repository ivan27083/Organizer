using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_test.Services;
using Xamarin_test.Views;
using Xamarin_test.Classes;
using System.IO;

namespace Xamarin_test
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "Missions.db";
        public static MissionRepository database;
        public static MissionRepository Database
        {

            get
            {
                if (database == null)

                {
                    database = new MissionRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }

        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
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
