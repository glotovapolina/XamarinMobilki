using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinToDoList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTaskPage : ContentPage
    {
        private readonly string userId;
        private readonly int categoryId;
        private readonly bool all;

        public CreateTaskPage(string userId, int categoryId, bool all)
        {
            InitializeComponent();
            InitializeText();

            this.userId = userId;
            this.categoryId = categoryId;
            this.all = all;
        }

        private void InitializeText()
        {
            Title = AppResources.CreateTask;

            NameLabel.Text = AppResources.TaskName;
            NameText.Placeholder = AppResources.TaskNamePlaceholder;
            NameText.PlaceholderColor = Color.LightGray;

            var now = DateTime.Now;
            DatePickerLabel.Text = AppResources.TaskDate;
            TaskDatePicker.Date = now;

            TimePickerLabel.Text = AppResources.TaskTime;
            TaskTimePicker.Time = now.TimeOfDay;

            DoneButton.Text = AppResources.Done;
        }

        private async void OnDoneButtonClicked(object sender, EventArgs args)
        {
            var name = NameText.Text;
            var datetime = TaskDatePicker.Date.ToString("yyyy-MM-dd") + " " + TaskTimePicker.Time.ToString(@"hh\:mm");

            if ((name == "") || name == null)
            {
                await App.Current.MainPage.DisplayAlert(AppResources.Error, AppResources.ErrorNameEmpty, AppResources.OK);
                return;
            }

            var task = new Task
            {
                Name = name,
                TimeDate = datetime,
                IdCategory = categoryId
            };
            await App.Database.SQLiteDatabase.InsertAsync(task);

            var mainPage = await TasksAndMenuDetail.Create(userId, categoryId, all);
            await Navigation.PushAsync(mainPage);
        }
    }
}