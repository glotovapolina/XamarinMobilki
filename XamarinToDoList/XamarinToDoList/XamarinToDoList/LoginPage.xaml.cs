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
            Title = "Authentication";
            loginViewModel = new LoginViewModel();
            BindingContext = loginViewModel;
        }
        protected override async void OnAppearing()
        {
            await App.Database.CreateTableUsers();
            await App.Database.CreateTableCategory();
            await App.Database.CreateTableTask();
           

            List<Users> users = await App.Database.GetItemsUsers();

            if (users.Count != 0)
            {
                TasksAndMenu page = new TasksAndMenu(users[0].Email);
                await Navigation.PushModalAsync(page);
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