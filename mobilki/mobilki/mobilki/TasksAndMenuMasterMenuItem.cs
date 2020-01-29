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

        public Type TargetType { get; set; }
    }
}