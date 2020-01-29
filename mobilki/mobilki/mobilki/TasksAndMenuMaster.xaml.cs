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

namespace mobilki
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksAndMenuMaster : ContentPage
    {
        public ListView ListView;

        public TasksAndMenuMaster()
        {
            InitializeComponent();

            BindingContext = new TasksAndMenuMasterViewModel();
            ListView = MenuItemsListView;
        }

        class TasksAndMenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<Page> MenuItems { get; set; }

            public TasksAndMenuMasterViewModel()
            {
                MenuItems = new ObservableCollection<Page>(new[]
                {
                    new TasksAndMenuMasterMenuItem { Title = "Page 1" },
                    new TasksAndMenuMasterMenuItem { Title = "Page 2" },
                    new TasksAndMenuMasterMenuItem { Title = "Page 3" },
                    new TasksAndMenuMasterMenuItem { Title = "Page 4" },
                    new TasksAndMenuMasterMenuItem { Title = "Page 5" },
                });
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
        }
    }
}