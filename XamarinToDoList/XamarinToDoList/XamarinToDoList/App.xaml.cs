using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinToDoList
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "database.db";
        private static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }

        public App()
        {
            // todo clean
            InitializeComponent();
            /*
            Database.SQLiteDatabase.DeleteAllAsync<Task>();
            Database.SQLiteDatabase.DeleteAllAsync<Category>();
            */
            MainPage = new NavigationPage(new LoginPage());
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
