﻿using System;
using System.Collections.Generic;
using System.Linq;
using T = System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinToDoList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksAndMenuDetail : ContentPage
    {
        public Type TargetType { get; set; }

        #region Extra info from other activities
        public String UserId;
        private bool all = true;
        public int? CategoryId = null;
        #endregion

        #region New task
        private String description;
        private String nameCategory;
        private DateTime? datetime = null;
        #endregion

        #region For tasks and categories
        private List<Task> tasks = new List<Task>();
        private List<Category> categories = new List<Category>();
        private Dictionary<Guid, Task> checkboxTaskPairs = new Dictionary<Guid, Task>();
        // private ArrayMap<Integer, String> menuCategory = new ArrayMap<>();
        //private Menu subMenu;
        #endregion

        public static async T.Task<TasksAndMenuDetail> Create(string userId, int? categoryId, bool all = false)
        {
            var page = new TasksAndMenuDetail();
            page.Set(userId, categoryId, all);
            await page.InitPage();

            return page;
        }

        //todo remove
        public TasksAndMenuDetail()
        {
            TargetType = typeof(TasksAndMenuDetail);
            InitializeComponent();
        }

        public TasksAndMenuDetail(string userId, int? categoryId, bool all = false)
        {
            InitializeComponent();
            Set(userId, categoryId, all);
            InitPage();
        }

        public void Set(string userId, int? categoryId, bool all = false)
        {
            this.UserId = userId;
            this.CategoryId = categoryId;
            // todo category == null? nocategory
            this.all = all;
        }

        public async T.Task InitPage()
        {
            InitTasks();

            await SetCategoryId();

            await SetCategoryNameInTitle();

            SortAndShow();
        }

        private async T.Task SetCategoryId()
        {
            // todo clean
            var count = (await App.Database.GetItemsCategory()).Count();
            var cat = await App.Database.SQLiteDatabase.Table<Category>().ToListAsync();

            if (all)
            {
                var undeletableName = Database.UndeletableCategory;
                var undeletableCategory = await App.Database.SQLiteDatabase.FindAsync<Category>(c => c.Name == undeletableName);
                CategoryId = undeletableCategory.IdCategory;
            }
        }

        private void SortAndShow()
        {
            string previousState = "";
            string currentState = "";

            ListLayout.Children.Clear();
            
            SetAlarms();
            SelectTasksInCurrentCategory();

            tasks = Task.SortFromSoonToLater(tasks);

            previousState = tasks.FirstOrDefault() == null ? "" : tasks.First().Timeline;

            foreach (var current in tasks)
            {
                currentState = current.Timeline;

                var taskLayout = CreateTaskLayout(current);

                if (AddTimelineIfChenged(previousState, currentState))
                {
                    previousState = currentState;
                }

                ListLayout.Children.Insert(0, taskLayout);
            }

            if (tasks.Count - 1 >= 0)
            {
                var firstTimeline = tasks[tasks.Count - 1].Timeline;
                Label firstTimelineLabel = CreateTimelineLabel(firstTimeline);
                ListLayout.Children.Insert(0, firstTimelineLabel);
            }

        }



        /// <summary>
        /// Все таски пользователя по всем категориям
        /// </summary>
        private async T.Task InitTasks()
        {
            // todo get tasks
            var userCategories = await App.Database.SQLiteDatabase.Table<Category>().Where(c => c.IdUser == UserId).ToListAsync();
            var categoriesIds = userCategories.Select(c => c.IdCategory).ToList();
            tasks = await App.Database.SQLiteDatabase.Table<Task>().Where(t => categoriesIds.Contains(t.IdCategory)).ToListAsync();
        }

        private async T.Task SetCategoryNameInTitle()
        {
            string categoryName;
            if (all)
            {
                categoryName = "All";
            }
            else
            {
                var factName = (await App.Database.GetItemCategory(CategoryId.Value)).Name;
                var undelName = Database.UndeletableCategory;
                categoryName = factName.Equals(undelName) ? "NoCategory from resource" : factName;
            }
            
            Title = categoryName;
        }

        private void CancelAlarms()
        {
            //cancel alarms
        }

        private void SetAlarms()
        {
            foreach(var task in tasks)
            {
                //set alarm
            }
        }

        private void SelectTasksInCurrentCategory()
        {
            if (!all)
                tasks = tasks.Where(task => task.IdCategory == CategoryId).ToList();
        }

        private void CheckboxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                CancelAlarms();

                // Remove task from local list of tasks
                tasks.Remove(checkboxTaskPairs[((CheckBox)sender).Id]);

                // Remove element from view
                var stack = (StackLayout)((CheckBox)sender).Parent;
                ListLayout.Children.Remove(stack);
                

                // Delete task from db
                // todo

                SortAndShow();
            }
        }

        private StackLayout CreateTaskLayout(Task current)
        {
            // Layout for row
            var checkboxAndLabelsLayout = new StackLayout();
            checkboxAndLabelsLayout.Orientation = StackOrientation.Horizontal;

            // Checkbox
            var checkbox = new CheckBox
            {
                Color = Color.CornflowerBlue
            };
            checkboxTaskPairs.Add(checkbox.Id, current);
            checkbox.CheckedChanged += CheckboxCheckedChanged;
            checkboxAndLabelsLayout.Children.Add(checkbox);

            // Label
            var label = new Label
            {
                Text = current.ToString(),
                FontSize = 16
            };
            checkboxAndLabelsLayout.Children.Add(label);

            return checkboxAndLabelsLayout;
        }

        private Label CreateTimelineLabel (string timeline)
        {
            Label timelineNameLabel = new Label
            {
                Text = timeline,
                FontSize = 20,
                // Todo otherday color
                TextColor = Color.DarkGray
            };

            if (timeline == Task.TODAY)
            {
                // Todo accent color instead of blue
                timelineNameLabel.TextColor = Color.CornflowerBlue;
            }
            else if (timeline == Task.EXPIRED)
            {
                // Todo expired color instead of red
                timelineNameLabel.TextColor = Color.Red;
            }
            else if (timeline == Task.TOMORROW)
            {
                // Todo tomorrow color instead of red
                timelineNameLabel.TextColor = Color.DarkSlateBlue;
            }

            return timelineNameLabel;
        }

        private bool AddTimelineIfChenged(string previousState, string currentState)
        {
            if (previousState != currentState)
            {
                Label timelineNameLabel = CreateTimelineLabel(previousState);
                ListLayout.Children.Insert(0, timelineNameLabel);
                return true;
            }

            return false;
        }

        private async void OnFabButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CreateTaskPage(UserId, CategoryId.Value, all));
        }
    }
}