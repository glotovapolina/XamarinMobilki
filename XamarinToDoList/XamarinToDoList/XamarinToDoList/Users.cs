using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinToDoList
{
    public class Users
    {
        [PrimaryKey, Column("Email")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
