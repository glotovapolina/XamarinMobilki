using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
        private string userId;

        public Settings(string userId)
        {
            InitializeComponent();
            this.userId = userId;
            Logout.Text = AppResources.Logout;
            Title = AppResources.Settings;
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            var user = await App.Database.GetItemUser(userId);
            await App.Database.SQLiteDatabase.DeleteAsync(user);
            await Navigation.PushAsync(new LoginPage());
        }
    }
}