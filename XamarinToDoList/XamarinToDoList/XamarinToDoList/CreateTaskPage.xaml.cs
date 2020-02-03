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
            Title = "Create new task";

            NameLabel.Text = "What task do you want to plan?";
            NameText.Placeholder = "I want to do ...";
            NameText.PlaceholderColor = Color.LightGray;

            var now = DateTime.Now;
            DatePickerLabel.Text = "What day it needs to be done?";
            TaskDatePicker.Date = now;

            TimePickerLabel.Text = "At what time that day you want to see notification?";
            TaskTimePicker.Time = now.TimeOfDay;

            DoneButton.Text = "Done";
        }

        private async void OnDoneButtonClicked(object sender, EventArgs args)
        {
            var name = NameText.Text;
            var datetime = TaskDatePicker.Date.ToString("yyyy-MM-dd") + " " + TaskTimePicker.Time.ToString(@"hh\:mm");

            if ((name == "") || name == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Name should not be empty.", "OK");
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