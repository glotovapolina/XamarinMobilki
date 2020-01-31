using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinToDoList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangesCategoryPage : ContentPage
    {
        string Email;
        public Type TargetType { get; set; }
        public ChangesCategoryPage(string email)
        {
            InitializeComponent();
            Title = "Список категорий";
            Email = email;
       //     Category category = new Category("Работа", 1, Email);
        //    App.Database.SaveItemCategory(category);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            categoryList.ItemsSource = await App.Database.GetItemsCategory();            
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