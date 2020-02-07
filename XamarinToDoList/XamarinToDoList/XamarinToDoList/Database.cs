using SQLite;
using System;
using System.Collections.Generic;
using T = System.Threading.Tasks;

namespace XamarinToDoList
{
    public class Database
    {
        public readonly SQLiteAsyncConnection SQLiteDatabase;
        // Name for NoCategory in DB, same for all users
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
            await SQLiteDatabase.CreateTableAsync<User>();
        }

        public async T.Task<IEnumerable<Category>> GetItemsCategory()
        {
            return await SQLiteDatabase.Table<Category>().ToListAsync();
        }
        public async T.Task<List<Task>> GetItemsTask()
        {
            return await SQLiteDatabase.Table<Task>().ToListAsync();
        }
        public async T.Task<List<User>> GetItemsUsers()
        {
            return await SQLiteDatabase.Table<User>().ToListAsync();
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
        public async void SaveItemUser(User user)
        {
            if (user != null)
            {
                await SQLiteDatabase.InsertAsync(user);
            }

        }
        public async T.Task<User> GetItemUser(string email)
        {
            try
            {
                return await SQLiteDatabase.GetAsync<User>(email);
            }
            catch (Exception e)
            {
                string ex = e.StackTrace.ToString();
                return null;
            }
        }
    }
}
