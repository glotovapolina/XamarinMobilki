using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobilki
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTaskPage : ContentPage
    {
        private readonly string userId;
        private readonly int? categoryId;

        public CreateTaskPage(string idUser, int? idCategory)
        {
            InitializeComponent();
            InitializeText();

            this.userId = idUser;
            this.categoryId = idCategory;
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

            if (name == "")
                MainLayout.Children.Add(new Label { Text = "Name should not be empty." });

            // todo add task to db
            //if category null - nocategory


            await Navigation.PushAsync(new TasksAndMenuDetail(userId, categoryId));
        }
    }
}