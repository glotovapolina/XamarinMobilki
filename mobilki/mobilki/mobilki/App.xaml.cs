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
            //todo remove hardcode
            MainPage = new TasksAndMenu("userId");
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
