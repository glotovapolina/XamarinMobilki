using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinToDoList;

namespace ToDoList
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
        
    {
        LoginViewModel loginViewModel;
        public LoginPage ()
		{
            InitializeComponent();
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
            base.OnAppearing();

            await App.Database.CreateTableUsers();
            await App.Database.CreateTableCategory();
            await App.Database.CreateTableTask();
           

            List<User> user = await App.Database.GetItemsUsers();

            if (user.Count != 0)
            {
                var main = await TasksAndMenu.Create(user[0].Email);
                await Navigation.PushModalAsync(main);
            }
            
        }
    }
}