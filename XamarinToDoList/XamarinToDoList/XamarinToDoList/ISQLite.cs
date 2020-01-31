using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinToDoList
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
