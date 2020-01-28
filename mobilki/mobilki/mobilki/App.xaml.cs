using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobilki
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TasksAndMenu();
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
