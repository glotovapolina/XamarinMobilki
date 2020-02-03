using SQLite;
using System;
using System.Collections.Generic;
using T = System.Threading.Tasks;

namespace XamarinToDoList
{
    public class Database
    {
        public readonly SQLiteAsyncConnection SQLiteDatabase;
        //todo dont change not resource
        public static readonly String UndeletableCategory = "NoCategory";

        public Database(string databasePath)
        {
            SQLiteDatabase = new SQLiteAsyncConnection(databasePath);

            // database.CreateTable<Category>();
            //    database.CreateTable<Task>();
            //   database.CreateTable<Users>();
        }

        public static String GetUndeletableCategory()
        {
            return UndeletableCategory;
        }
        public async T.Task CreateTableCategory()
        {
            await SQLiteDatabase.CreateTableAsync<Category>();
        }
        public async T.Task CreateTableTask()
        {
            await SQLiteDatabase.CreateTableAsync<Task>();
        }
        public async T.Task CreateTableUsers()
        {
            await SQLiteDatabase.CreateTableAsync<Users>();
        }

        public async T.Task<IEnumerable<Category>> GetItemsCategory()
        {
            return await SQLiteDatabase.Table<Category>().ToListAsync();
        }
        public async T.Task<List<Task>> GetItemsTask()
        {
            return await SQLiteDatabase.Table<Task>().ToListAsync();
        }
        public async T.Task<List<Users>> GetItemsUsers()
        {
            return await SQLiteDatabase.Table<Users>().ToListAsync();
        }
        public async T.Task<Category> GetItemCategory(int id)
        {
            return await SQLiteDatabase.GetAsync<Category>(id);
        }
        public async T.Task<Task> GetItemTask(int id)
        {
            return await SQLiteDatabase.GetAsync<Task>(id);
        }
        public async T.Task<int> DeleteItemCategory(int id)
        {
            return await SQLiteDatabase.DeleteAsync<Category>(id);
        }
        public async T.Task<int> DeleteItemTask(int id)
        {
            return await SQLiteDatabase.DeleteAsync<Task>(id);
        }
        public async T.Task<int> SaveItemCategory(Category category)
        {
            if (category.IdCategory != 0)
            {
                await SQLiteDatabase.UpdateAsync(category);
                return category.IdCategory;
            }
            else
            {
                return await SQLiteDatabase.InsertAsync(category);
            }
        }
        public async T.Task<int> SaveItemTask(Task task)
        {
            if (task.IdTask != 0)
            {
                return await SQLiteDatabase.InsertAsync(task);

            }
            else
            {
                return await SQLiteDatabase.InsertAsync(task);
            }
        }
        public async void SaveItemUser(Users user)
        {
            if (user != null)
            {
                //  database.Update(user);
                //   return user.Email.Length;
                //}
                // else
                // {
                await SQLiteDatabase.InsertAsync(user);
            }

        }
        public async T.Task<Users> GetItemUser(string email)
        {
            try
            {
                return await SQLiteDatabase.GetAsync<Users>(email);
            }
            catch (Exception e)
            {
                string ex = e.StackTrace.ToString();
                return null;
            }
        }
    }
}
