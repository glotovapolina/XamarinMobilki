using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobilki
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksAndMenuDetail : ContentPage
    {
        #region For saving instance
        private bool isEnteringText = false;
        private bool isEnteringCategory = false;
        private bool isPickingDate = false;
        private bool isPickingTime = false;
        private String enteredText = null;
        private String enteredCategory = null;
        private DateTime? pickedTime = null;
        private DateTime? pickedDate = null;
        #endregion

        #region Extra info from other activities
        private String userId;
        private bool all = true;
        private int idCategory = 1;
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

        public TasksAndMenuDetail()
        {
            InitializeComponent();
            Title = "All";
            InitPage(-1);
        }

        public TasksAndMenuDetail(int categoryId)
        {
            InitializeComponent();
            InitPage(categoryId);
        }

        public void InitPage(int categoryId)
        {
            InitCategories();
            InitTasks();

            SetCategoryNameInTitle();

            SortAndShow();
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

                var taskLayout = CreateTaskToLayout(current);

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
        /// Все категории пользователя
        /// </summary>
        private void InitCategories()
        {
            categories.Add(new Category
            {
                IdCategory = 1,
                Name = "c111",
                IdUser = "1"
            });
            categories.Add(new Category
            {
                IdCategory = 2,
                Name = "c222",
                IdUser = "1"
            });
        }

        /// <summary>
        /// Все таски пользователя по всем категориям
        /// </summary>
        private void InitTasks()
        {
            tasks.Add(new Task
            {
                IdTask = 1,
                IdCategory = 1,
                Name = "111",
                TimeDate = "2020-01-29 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 2,
                IdCategory = 1,
                Name = "222",
                TimeDate = "2020-01-30 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 3,
                IdCategory = 1,
                Name = "333",
                TimeDate = "2020-01-31 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 4,
                IdCategory = 1,
                Name = "444",
                TimeDate = "2021-10-01 11:00"
            });

            #region copies
            tasks.Add(new Task
            {
                IdTask = 1,
                IdCategory = 1,
                Name = "111",
                TimeDate = "2020-01-29 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 2,
                IdCategory = 1,
                Name = "222",
                TimeDate = "2020-01-30 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 3,
                IdCategory = 1,
                Name = "333",
                TimeDate = "2020-01-31 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 4,
                IdCategory = 1,
                Name = "444",
                TimeDate = "2021-10-01 11:00"
            }); tasks.Add(new Task
            {
                IdTask = 1,
                IdCategory = 1,
                Name = "111",
                TimeDate = "2020-01-29 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 2,
                IdCategory = 1,
                Name = "222",
                TimeDate = "2020-01-30 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 3,
                IdCategory = 1,
                Name = "333",
                TimeDate = "2020-01-31 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 4,
                IdCategory = 1,
                Name = "444",
                TimeDate = "2021-10-01 11:00"
            });

            tasks.Add(new Task
            {
                IdTask = 1,
                IdCategory = 1,
                Name = "111",
                TimeDate = "2020-01-29 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 2,
                IdCategory = 1,
                Name = "222",
                TimeDate = "2020-01-30 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 3,
                IdCategory = 1,
                Name = "333",
                TimeDate = "2020-01-31 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 4,
                IdCategory = 1,
                Name = "444",
                TimeDate = "2021-10-01 11:00"
            }); tasks.Add(new Task
            {
                IdTask = 1,
                IdCategory = 1,
                Name = "111",
                TimeDate = "2020-01-29 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 2,
                IdCategory = 1,
                Name = "222",
                TimeDate = "2020-01-30 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 3,
                IdCategory = 1,
                Name = "333",
                TimeDate = "2020-01-31 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 4,
                IdCategory = 1,
                Name = "444",
                TimeDate = "2021-10-01 11:00"
            });
            #endregion

            tasks.Add(new Task
            {
                IdTask = 5,
                IdCategory = 2,
                Name = "555",
                TimeDate = "2020-10-01 11:00"
            });
        }

        private void SetCategoryNameInTitle()
        {
            /*
            String catName = all ? getString(R.string.all_tasks) : db.getCategoryNameById(idCategory);
            catName = (catName.equals(getString(R.string.no_category_in_db))) ? getString(R.string.no_category) : catName;
            return catName;
            */
            Title = categories.First(c => c.IdCategory == idCategory).Name;
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
            tasks = tasks.Where(task => task.IdCategory == idCategory).ToList();
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

        private StackLayout CreateTaskToLayout(Task current)
        {
            // Layout for row
            var checkboxAndLabelsLayout = new StackLayout();
            checkboxAndLabelsLayout.Orientation = StackOrientation.Horizontal;

            // Checkbox
            var checkbox = new CheckBox();
            checkboxTaskPairs.Add(checkbox.Id, current);
            checkbox.CheckedChanged += CheckboxCheckedChanged;
            checkboxAndLabelsLayout.Children.Add(checkbox);

            // Label
            var label = new Label
            {
                Text = current.ToString()
            };
            checkboxAndLabelsLayout.Children.Add(label);

            return checkboxAndLabelsLayout;
        }

        private Label CreateTimelineLabel (string timeline)
        {
            Label timelineNameLabel = new Label
            {
                Text = timeline,
                FontSize = 16,
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
            Title = Guid.NewGuid().ToString();
        }
    }
}