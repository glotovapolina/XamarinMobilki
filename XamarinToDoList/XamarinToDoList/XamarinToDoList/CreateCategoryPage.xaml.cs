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
            Title = AppResources.CreateNewCategory;

            NameLabel.Text = AppResources.NameCategory;
            NameText.Placeholder = AppResources.NameCategoryPlaceholder;
            NameText.PlaceholderColor = Color.LightGray;

            DoneButton.Text = AppResources.Done;
        }

        private async void OnDoneButtonClicked(object sender, EventArgs args)
        {
            var name = NameText.Text;

            if ((name == "") || name == null)
            {
                await App.Current.MainPage.DisplayAlert(AppResources.Error, AppResources.ErrorNameEmpty, AppResources.OK);
                return;
            }

            var category = new Category
            {
                Name = name,
                IdUser = userId
            };
            await App.Database.SQLiteDatabase.InsertAsync(category);

            var mainPage = await TasksAndMenu.Create(userId);
            App.Current.MainPage = mainPage;
        }
    }
}