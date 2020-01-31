using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinToDoList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksAndMenu : MasterDetailPage
    {
        public TasksAndMenu(string userId)
        {
            InitializeComponent();
            MasterPage.SetMenu(userId);

            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

            var detail = (TasksAndMenuDetail)((NavigationPage)Detail).RootPage;
            detail.UserId = userId;
            detail.CategoryId = null;
            detail.InitPage();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as TasksAndMenuDetail;
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