using System;
using Xamarin.Forms;

namespace mobilki
{

    public class TasksAndMenuMasterMenuItem : TasksAndMenuDetail
    {
        public TasksAndMenuMasterMenuItem()
        {
            TargetType = typeof(TasksAndMenuMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}