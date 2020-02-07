using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinToDoList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangesCategoryPage : ContentPage
    {
        string Email;
        public Type TargetType { get; set; }
        public ChangesCategoryPage(string email)
        {
            InitializeComponent();
            Title = AppResources.ChangeCategories;
            Email = email;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            categoryList.ItemsSource = await App.Database.GetItemsCategory();            
        }
        private void BtnDone_Activated(object sender, EventArgs e)
        {

        }
    

        private void BtnClear_Activated(object sender, EventArgs e)
        {

        }

    }
}