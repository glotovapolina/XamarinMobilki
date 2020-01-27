using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ToDoList.Droid
{
    public class HelperAndroid
    {
        bool flag = true;
        public void createDatabaseAndroid()
        {

            /* SQLite_Android sql = new SQLite_Android();
             string pathDB = sql.GetDatabasePath("database");
             Database db= new Database();
             if (!File.Exists(pathDB))
             {
                  db = new Database(pathDB);
             }
             else { 

                 Users user = new Users();
                 if (flag)
                 {
                     db.SaveItemUser(user);
                     flag = false;
                 }
                 else
                 {
                     WelcomePage w = new WelcomePage(user.Email,user.Password);
                 }
             }
         } 
     }*/
        }
    }
}