using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
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
    }
}
