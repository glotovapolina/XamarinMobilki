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
           

            List<Users> user = await App.Database.GetItemsUsers();

            if (user.Count != 0)
            {
                TasksAndMenu page = new TasksAndMenu(user[0].Email);
                await Navigation.PushModalAsync(page);
                /*
                ChangesCategoryPage categPage = new ChangesCategoryPage(user[0].Email, user[0].Password);
                categPage.BindingContext = user;
                await Navigation.PushAsync(categPage);
                */
                //   App.Database.SaveItemUser(user[0]);

            }
            /*    else
                {
                    WelcomePage welcPage = new WelcomePage(user[0].Email,user[0].Password);
                    welcPage.BindingContext = user;
                    Navigation.PushAsync(welcPage);
                }*/
            base.OnAppearing();
        }
    }
}