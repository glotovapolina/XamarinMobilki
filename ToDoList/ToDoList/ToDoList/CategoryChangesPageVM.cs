using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace ToDoList
{
  public  class CategoryChangesPageVM //: INotifyPropertyChanged
    {
       // public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ObservableCollection<Category> Categorys { get; set; }
        public Command<Category> RemoveCommand
        {
            get
            {
                return new Command<Category>((category) =>
                {
                    Categorys.Remove(category);
                });
            }
        }

       public CategoryChangesPageVM(List<Category> listcurrentcategory)
       {
           Categorys = new ObservableCollection<Category> (listcurrentcategory);
       }
        public CategoryChangesPageVM()
        {
            Categorys = new ObservableCollection<Category>();
        }

    }
}
