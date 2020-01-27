using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
