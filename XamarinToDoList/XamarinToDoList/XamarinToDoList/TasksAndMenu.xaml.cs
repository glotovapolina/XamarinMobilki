using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T = System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinToDoList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksAndMenu : MasterDetailPage
    {
        public TasksAndMenu()
        {
            InitializeComponent();            
        }

        public static async T.Task<TasksAndMenu> Create(string userId)
        {
            var main = new TasksAndMenu();
            await main.MasterPage.SetMenu(userId);

            main.MasterPage.ListView.ItemSelected += main.ListView_ItemSelected;

            var detail = (TasksAndMenuDetail)((NavigationPage)main.Detail).RootPage;
            detail.UserId = userId;
            detail.CategoryId = null;
            await detail.InitPage();

            return main;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as ContentPage;
            if (item == null)
                return;

            var page = (Page)item;
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}