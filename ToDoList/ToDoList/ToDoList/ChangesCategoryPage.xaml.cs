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
      //  List<Category> listdeletecategory;
       // List<Category> listcurrentcategory;
        public ChangesCategoryPage(string email, string pass)
        {
            InitializeComponent();
            Title = "Список категорий";
            Email = email;
            Pass = pass;
           // listdeletecategory = new List<Category>();
            //listcurrentcategory = new List<Category>();

            //     Category category = new Category("Работа", 1, Email);
            //    App.Database.SaveItemCategory(category);
        }
        protected override async void OnAppearing()
        {
          //  if (listdeletecategory.Count == 0)
           // {
                
               
              //  categoryChangesPageVM = new CategoryChangesPageVM(listcurrentcategory);
                //BindingContext = categoryChangesPageVM;

                categoryList.ItemsSource = await App.Database.GetItemsCategory(); 
                base.OnAppearing();
            //}
        }
        private async void BtnDone_Activated(object sender, EventArgs e)
        {
          //  for(int i=0;i<listdeletecategory.Count;i++ )
           
        }

     
        private void BtnClear_Activated(object sender, EventArgs e)
        {

        }

        private async void  Btndel_Clicked(object sender, EventArgs e)
        {
            
        //    var b = (Button)sender;
            Button button = sender as Button;
            Category category = button.BindingContext as Category;
            await App.Database.DeleteItemCategory(category);
            await this.Navigation.PopAsync();
          //  CategoryChangesPageVM vm = BindingContext as CategoryChangesPageVM;
            // Category category = b.CommandParameter as Category;


            //listdeletecategory.Add(category);
            //listcurrentcategory.Remove(category);


          //  categoryList.ItemsSource = listcurrentcategory;
            //var ddd = categoryList.ItemsSource;
          //  vm.RemoveCommand.Execute(category);
            // bool v = listcurrentcategory.Remove(category);

           // this.Navigation.PopAsync();

            // App.Database.DeleteItemCategory();
        }
      
        /* protected override void OnAppearing()
{
ToolbarItem.
base.OnAppearing();


}*/

    }
}