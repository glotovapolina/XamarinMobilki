using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinToDoList;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ToDoList
{
    public partial class App : Application
    {
        public static TasksAndMenuMaster CurrentMaster;
        public const string DATABASE_NAME = "database.db";
        public static Database database;
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
            InitializeComponent();
          //  MainPage = new NavigationPage(new MapPage()); 
            MainPage = new NavigationPage(new LoginPage());
            //  MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
