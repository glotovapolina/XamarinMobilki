using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobilki
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksAndMenuDetail : ContentPage
    {
        public string Title { get; set; }
        List<Task> tasks = new List<Task>();
        List<Category> categories = new List<Category>();
        public TasksAndMenuDetail()
        {
            Title = "All";
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

            tasks.Add(new Task
            {
                IdTask = 1,
                IdCategory = 1, 
                Name = "111",
                TimeDate = "2020-10-01 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 2,
                IdCategory = 1,
                Name = "222",
                TimeDate = "2020-10-01 11:00"
            });
            tasks.Add(new Task
            {
                IdTask = 3,
                IdCategory = 2,
                Name = "333",
                TimeDate = "2020-10-01 11:00"
            });


            InitializeComponent();
            foreach (var t in tasks)
            {
                var stack = new StackLayout();
                stack.Orientation = StackOrientation.Horizontal;
                stack.Children.Add(new CheckBox());
                var l = new Label();
                l.Text = t.Name + "\n" + t.TimeDate;
                stack.Children.Add(l);
                ListLayout.Children.Add(stack);
            }            
        }
    }
}