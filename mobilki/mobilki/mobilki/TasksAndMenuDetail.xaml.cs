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
            SortAndShow();

            foreach (var t in tasks)
            {
                var stack = new StackLayout();
                stack.Orientation = StackOrientation.Horizontal;
                var c = new CheckBox();
                // todo add c and t and stack to object
                // find way checketcheckbox get id
                stack.Children.Add(c);
                var vvv = c.Parent;
                var l = new Label();
                l.Text = t.ToString();
                stack.Children.Add(l);
                ListLayout.Children.Add(stack);
            }
        }

        private void SortAndShow()
        {
            int previousState = 0;
            int currentState = 0;

            ListLayout.Children.Clear();
            //cancelAlarms();
            tasks.Clear();

            InitCategories();
            InitTasks();

            SetCategoryNameInTitle();
            CancelAlarms();
            SetAlarms();
            SetTasksInCurrentCategory();



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
                TimeDate = "2020-10-01 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 2,
                IdCategory = 1,
                Name = "222",
                TimeDate = "2020-10-01 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 3,
                IdCategory = 2,
                Name = "333",
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

        private void SetTasksInCurrentCategory()
        {
            tasks = tasks.Where(task => task.IdCategory == idCategory).ToList();
        }
    }
}