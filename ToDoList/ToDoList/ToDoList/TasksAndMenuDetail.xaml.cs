using System;
using System.Collections.Generic;
using System.Linq;
using T = System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;
using ToDoList;

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

        #region For tasks and categories
        private List<Task> tasks = new List<Task>();
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

        public TasksAndMenuDetail()
        {
            TargetType = typeof(TasksAndMenuDetail);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await InitPage();
        }

        public void Set(string userId, int? categoryId, bool all = false)
        {
            this.UserId = userId;
            this.CategoryId = categoryId;
            this.all = all;
        }

        public async T.Task InitPage()
        {
            await InitTasks();

            await SetCategoryId();

            await SetCategoryNameInTitle();

            SortAndShow();
        }

        private async T.Task SetCategoryId()
        {
            if (all)
            {
                var undeletableName = Database.UndeletableCategory;
                var undeletableCategory = await App.Database.SQLiteDatabase
                    .FindAsync<Category>(c => 
                    (c.Name == undeletableName) && (c.IdUser == UserId));
                CategoryId = undeletableCategory.IdCategory;
            }
        }

        private void SortAndShow()
        {
            string previousState = "";
            string currentState = "";

            ListLayout.Children.Clear();

            CancelAlarms();
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
            // check clean get tasks
            var tr = await App.Database.SQLiteDatabase.Table<Task>().ToListAsync();
            var userCategories = await App.Database.SQLiteDatabase.Table<Category>().Where(c => c.IdUser == UserId).ToListAsync();
            var categoriesIds = userCategories.Select(c => c.IdCategory).ToList();
            tasks = await App.Database.SQLiteDatabase.Table<Task>().Where(t => categoriesIds.Contains(t.IdCategory)).ToListAsync();
        }

        private async T.Task SetCategoryNameInTitle()
        {
            string categoryName;
            if (all)
            {
                categoryName = AppResources.All;
            }
            else
            {
                var factName = (await App.Database.GetItemCategory(CategoryId.Value)).Name;
                var undelName = Database.UndeletableCategory;
                categoryName = factName.Equals(undelName) ? AppResources.NoCategory : factName;
            }

            Title = categoryName;
        }

        private void CancelAlarms()
        {
            foreach (var t in tasks)
            {
                CrossLocalNotifications.Current.Cancel(t.IdTask);
            }
        }

        private void SetAlarms()
        {
            foreach (var t in tasks)
            {
                if (t.DateTimeOfTask > DateTime.Now)
                    CrossLocalNotifications.Current.Show(t.Name, AppResources.NotificationText, t.IdTask, t.DateTimeOfTask);
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
                var taskToDelete = checkboxTaskPairs[((CheckBox)sender).Id];

                CrossLocalNotifications.Current.Cancel(taskToDelete.IdTask);

                // check Delete task from db
                App.Database.DeleteItemTask(taskToDelete.IdTask);

                // Remove task from local list of tasks
                tasks.Remove(taskToDelete);

                // Remove element from view
                var stack = (StackLayout)((CheckBox)sender).Parent;
                ListLayout.Children.Remove(stack);

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

        private Label CreateTimelineLabel(string timeline)
        {
            Label timelineNameLabel = new Label
            {
                Text = timeline,
                FontSize = 20,
                TextColor = Color.DarkGray
            };

            if (timeline == Task.TODAY)
            {
                timelineNameLabel.TextColor = Color.CornflowerBlue;
            }
            else if (timeline == Task.EXPIRED)
            {
                timelineNameLabel.TextColor = Color.Red;
            }
            else if (timeline == Task.TOMORROW)
            {
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