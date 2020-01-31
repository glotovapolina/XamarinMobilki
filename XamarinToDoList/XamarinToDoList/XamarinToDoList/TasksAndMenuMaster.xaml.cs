using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinToDoList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksAndMenuMaster : ContentPage
    {
        public ListView ListView;

        public TasksAndMenuMaster()
        {
            InitializeComponent();
        }

        public void SetMenu(string userId)
        {
            BindingContext = new TasksAndMenuMasterViewModel(userId);
            ListView = MenuItemsListView;
        }

        class TasksAndMenuMasterViewModel : INotifyPropertyChanged
        {

            private List<Category> categories = new List<Category>();
            public ObservableCollection<Page> MenuItems { get; set; }

            public TasksAndMenuMasterViewModel(string userId)
            {
                InitCategories(userId);

                MenuItems = new ObservableCollection<Page>();
                // All category
                MenuItems.Add(new TasksAndMenuDetail(userId, null, true));
                // Other categories
                foreach (var category in categories)
                {
                    MenuItems.Add(new TasksAndMenuDetail(userId, category.IdCategory));
                }
                // Create category
                MenuItems.Add(new CreateCategoryPage(userId));
                // Change categories
                MenuItems.Add(new ChangesCategoryPage(userId));
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion

            /// <summary>
            /// Все категории пользователя
            /// </summary>
            private void InitCategories(string userId)
            {
                // todo load cetegories 

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
        }
    }
}