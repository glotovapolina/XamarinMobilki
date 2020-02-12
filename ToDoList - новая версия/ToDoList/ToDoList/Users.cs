using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, Column("Email")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
