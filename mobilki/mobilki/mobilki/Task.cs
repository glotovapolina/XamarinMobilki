using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobilki
{
    [Table("Task")]
    public class Task
    {
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

            for (int i = tasks.Count; i >= 1; i--) {
                for (int j = 0; j < i; j++) {
                    if (tasks[j].DateTimeOfTask > tasks[j +1].DateTimeOfTask)
                    {
                        bucket = tasks[j];
                        tasks.RemoveAt(j);
                        tasks.Insert(j +1, bucket);
                    }
                }
            }
            return tasks;
        }
    }
}
