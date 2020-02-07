using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinToDoList
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
        
    {
        LoginViewModel loginViewModel;
        public LoginPage ()
		{
			InitializeComponent ();
            Title = AppResources.Authentication;
            loginViewModel = new LoginViewModel();
            BindingContext = loginViewModel;
            Email.Placeholder = AppResources.Email;
            Password.Placeholder = AppResources.Password;
            registbtn.Text = AppResources.SignUp;
            loginbtn.Text = AppResources.Login;
        }
        protected override async void OnAppearing()
        {
            /*
            await App.Database.SQLiteDatabase.DeleteAllAsync<User>();
            await App.Database.SQLiteDatabase.DeleteAllAsync<Category>();
            await App.Database.SQLiteDatabase.DeleteAllAsync<Task>();
            */

            await App.Database.CreateTableUsers();
            await App.Database.CreateTableCategory();
            await App.Database.CreateTableTask();
           

            List<User> users = await App.Database.GetItemsUsers();

            if (users.Count != 0)
            {
                var main = await TasksAndMenu.Create(users[0].Email);
                await Navigation.PushModalAsync(main);
                /*
                ChangesCategoryPage categPage = new ChangesCategoryPage(users[0].Email, users[0].Password);
                categPage.BindingContext = users;
                await Navigation.PushAsync(categPage);
                */
                //   App.Database.SaveItemUser(users[0]);
            }
            /*    else
                {
                    WelcomePage welcPage = new WelcomePage(users[0].Email,users[0].Password);
                    welcPage.BindingContext = users;
                    Navigation.PushAsync(welcPage);
                }*/
            base.OnAppearing();
        }
    }
}