using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using T = System.Threading.Tasks;

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

        public async T.Task SetMenu(string userId)
        {
            var masterVM = await TasksAndMenuMasterViewModel.Create(userId);
            BindingContext = masterVM;
            ListView = MenuItemsListView;
        }

        class TasksAndMenuMasterViewModel : INotifyPropertyChanged
        {

            private List<Category> categories = new List<Category>();
            public ObservableCollection<Page> MenuItems { get; set; }

            public static async T.Task<TasksAndMenuMasterViewModel> Create(string userId)
            {
                var vm = new TasksAndMenuMasterViewModel();
                await vm.InitMaster(userId);
                return vm;
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

            public async T.Task InitMaster(string userId)
            {
                await InitCategories(userId);

                MenuItems = new ObservableCollection<Page>();
                // All category
                var allCategories = await TasksAndMenuDetail.Create(userId, null, true);
                MenuItems.Add(allCategories);
                // Other categories
                foreach (var category in categories)
                {
                    var otherCategory = await TasksAndMenuDetail.Create(userId, category.IdCategory, false);
                    MenuItems.Add(otherCategory);
                }
                // Create category
                MenuItems.Add(new CreateCategoryPage(userId));
                // Change categories
                MenuItems.Add(new ChangesCategoryPage(userId));
            }

            /// <summary>
            /// Все категории пользователя
            /// </summary>
            private async T.Task InitCategories(string userId)
            {
                // Add NoCategory
                if ((await App.Database.SQLiteDatabase.Table<Category>().Where(c => c.IdUser == userId).CountAsync() == 0)
                    || (await App.Database.SQLiteDatabase.Table<Category>().Where(c => c.Name == Database.UndeletableCategory).CountAsync() == 0))
                {
                    await App.Database.SaveItemCategory(new Category
                    {
                        Name = Database.UndeletableCategory,
                        IdUser = userId
                    });

                }

                // Load categories
                categories = await App.Database.SQLiteDatabase.Table<Category>().Where(c => c.IdUser == userId).ToListAsync();
            }
        }
    }
}