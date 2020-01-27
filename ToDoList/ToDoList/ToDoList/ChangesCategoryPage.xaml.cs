using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangesCategoryPage : ContentPage
    {
        CategoryChangesPageVM categoryChangesPageVM;
        string Email;
        string Pass;
        public ChangesCategoryPage(string email, string pass)
        {
            InitializeComponent();
            categoryChangesPageVM = new CategoryChangesPageVM();
            BindingContext = categoryChangesPageVM;
            Title = "Список категорий";
            Email = email;
            Pass = pass;
       //     Category category = new Category("Работа", 1, Email);
        //    App.Database.SaveItemCategory(category);
        }
        protected override async void OnAppearing()
        {
            
            categoryList.ItemsSource = await App.Database.GetItemsCategory();
            base.OnAppearing();
        }
        private void BtnDone_Activated(object sender, EventArgs e)
        {

        }
    

        private void BtnClear_Activated(object sender, EventArgs e)
        {

        }
        /* protected override void OnAppearing()
{
ToolbarItem.
base.OnAppearing();


}*/

    }
}