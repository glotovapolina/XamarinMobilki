using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobilki
{
    [Table("Task")]
    public class Task
    {
        public static readonly string EXPIRED = "EXPIRED";
        public static readonly string TODAY = "TODAY";
        public static readonly string TOMORROW = "TOMORROW";
        public static readonly string LATER = "LATER";

        public String Name { get; set; }
        [PrimaryKey, AutoIncrement, Column("_idTask")]
        public int IdTask { get; set; }
        public int IdCategory { get; set; }
        public String TimeDate { get; set; }

        public Task()
        {
        }

        public Task(String name, int idCategory, String timeDate)
        {
            this.Name = name;
            this.IdTask = IdTask;
            this.IdCategory = idCategory;
            this.TimeDate = timeDate;
        }

        public Task(int idTask, String name, int idCategory, String timeDate)
        {
            this.Name = name;
            this.IdTask = idTask;
            this.IdCategory = idCategory;
            this.TimeDate = timeDate;
        }

        public DateTime DateTimeOfTask
        {
            get => DateTime.Parse(TimeDate);
        }

        override
        public string ToString()
        {
            return Name + "\n" + DateTimeOfTask.ToString("HH:mm dd-MM-yyyy");
        }

        public static List<Task> SortFromSoonToLater(List<Task> tasks)
        {
            Task bucket;

            for (int i = 0; i < tasks.Count; i++)
            {
                for (int j = 0; j < tasks.Count - i -1; j++)
                {
                    if (tasks[j].DateTimeOfTask < tasks[j + 1].DateTimeOfTask)
                    {
                        bucket = tasks[j];
                        tasks.RemoveAt(j);
                        tasks.Insert(j + 1, bucket);
                    }
                }
            }
            return tasks;
        }

        private static bool FallsIntoInterval(
            DateTime item,
            DateTime intervalStart,
            DateTime intervalEnd)
        {

            return ((item >= intervalStart) && (item < intervalEnd));
        }

        public string Timeline
        {
            get
            {
                var now = DateTime.UtcNow;
                if ((DateTimeOfTask - now).TotalMilliseconds < 0)
                    return EXPIRED;

                var today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
                var tomorrow = today.AddDays(1);

                if (FallsIntoInterval(DateTimeOfTask, today, tomorrow))
                    return TODAY;

                var afterTomorrow = tomorrow.AddDays(1);
                if (FallsIntoInterval(DateTimeOfTask, tomorrow, afterTomorrow))
                    return TOMORROW;

                return LATER;
            }
        }
    }
}
