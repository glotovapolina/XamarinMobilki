using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace XamarinToDoList
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public LoginViewModel()
        {

        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        private string confirmpassword;
        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set
            {
                confirmpassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }
        public Command LoginCommand
        {
            get
            {
                return new Command(Login);
            }
        }

        private async void Login()
        {
            //null or empty field validation, check weather email and password is null or empty    

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {
                //call GetUser function which we define in Firebase helper class    
                var user = await FirebaseHelper.GetUser(Email);
                //firebase return null valuse if user data not found in database    
                if (user != null)
                    if (Email == user.Email && Password == user.Password)
                    {
                        //await App.Current.MainPage.DisplayAlert("Login Success", "", "Ok");
                        //Navigate to Wellcom page after successfuly login    
                        //pass user email to welcom page    
                        try
                        {
                            var user1 = await App.Database.GetItemUser(Email);

                            if (user1 == null)
                            {
                                App.Database.SaveItemUser(user);
                            }
                            // await App.Current.MainPage.Navigation.PushAsync(new ChangesCategoryPage(Email, Password));
                            var main = await TasksAndMenu.Create(user.Email);
                            await App.Current.MainPage.Navigation.PushModalAsync(main);
                        }
                        catch (Exception e)
                        {
                            await App.Current.MainPage.DisplayAlert("Login Fail", e.StackTrace.ToString(), "OK");
                        }
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                else
                    await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
            }
        }
        public Command SignUpCommand
        {
            get
            {
                return new Command(() =>
                {
                    // if (Password == ConfirmPassword)
                    SignUp();
                    // else
                    //   App.Current.MainPage.DisplayAlert("", "Password must be same as above!", "OK");
                });
            }
        }

        private async void SignUp()
        {
            //call GetUser function which we define in Firebase helper class    
            var user1 = await FirebaseHelper.GetUser(Email);
            //firebase return null valuse if user data not found in database    

            if (user1 != null)
            {
                await App.Current.MainPage.DisplayAlert("Login Fail", "Such user already exists", "OK");
            }
            else
            {
                //null or empty field validation, check weather email and password is null or empty    

                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                    await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
                else
                {
                    //call AddUser function which we define in Firebase helper class    
                    var user = await FirebaseHelper.AddUser(Email, Password);
                    //AddUser return true if data insert successfuly     
                    if (user)
                    {
                        await App.Current.MainPage.DisplayAlert("SignUp Success", "", "Ok");
                        //Navigate to Wellcom page after successfuly SignUp    
                        //pass user email to welcom page
                        var main = await TasksAndMenu.Create(Email);
                        await App.Current.MainPage.Navigation.PushModalAsync(main);
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Error", "SignUp Fail", "OK");

                }
            }
        }

    }
}
