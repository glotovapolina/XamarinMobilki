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
        string Email;

        public ChangesCategoryPage(string email)
        {
            InitializeComponent();
            Title = AppResources.ChangeCategories;
            Email = email;
        }
        protected override async void OnAppearing()
        {
            //  if (listdeletecategory.Count == 0)
            // {


            //  categoryChangesPageVM = new CategoryChangesPageVM(listcurrentcategory);
            //BindingContext = categoryChangesPageVM;
            base.OnAppearing();
            categoryList.ItemsSource = await App.Database.SQLiteDatabase.Table<Category>().Where(c => c.IdUser.Equals(Email)).ToListAsync();

        }
        private  void BtnDone_Activated(object sender, EventArgs e)
        {
          //  for(int i=0;i<listdeletecategory.Count;i++ )
           
        }

     
        private void BtnClear_Activated(object sender, EventArgs e)
        {

        }

        private async void  Btndel_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Category category = button.BindingContext as Category;
            if (!category.Name.Equals(Database.UndeletableCategory))
            {
                await App.Database.SQLiteDatabase.DeleteAsync(category);
                await App.CurrentMaster.SetMenu(Email);
                await this.Navigation.PushAsync(new ChangesCategoryPage(Email));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(AppResources.Error, AppResources.ErrorNoCategory, AppResources.OK);
            }
        }
      
        /* protected override void OnAppearing()
{
ToolbarItem.
base.OnAppearing();


}*/

    }
}