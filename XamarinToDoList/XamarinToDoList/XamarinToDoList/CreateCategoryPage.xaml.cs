using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinToDoList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateCategoryPage : ContentPage
    {
        private readonly string userId;

        public CreateCategoryPage(string userId)
        {
            InitializeComponent();
            InitializeText();
            this.userId = userId;
        }

        private void InitializeText()
        {
            Title = "Create new category";

            NameLabel.Text = "What category do you want to create?";
            NameText.Placeholder = "I want to create category ...";
            NameText.PlaceholderColor = Color.LightGray;

            DoneButton.Text = "Done";
        }

        private async void OnDoneButtonClicked(object sender, EventArgs args)
        {
            var name = NameText.Text;

            if ((name == "") || name == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Name should not be empty.", "OK");
                return;
            }

            var category = new Category
            {
                Name = name,
                IdUser = userId
            };
            await App.Database.SQLiteDatabase.InsertAsync(category);

            await Navigation.PushAsync(new TasksAndMenuDetail(userId, null, true));
        }
    }
}