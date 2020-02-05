using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinToDoList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        private string userId;

        public Settings(string userId)
        {
            InitializeComponent();
            this.userId = userId;
            Logout.Text = "Logout";
            Title = "Settings";
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {

        }
    }
}