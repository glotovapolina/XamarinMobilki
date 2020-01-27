using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public  class Database
    {
        SQLiteAsyncConnection database;
     
        public Database(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);

           // database.CreateTable<Category>();
        //    database.CreateTable<Task>();
         //   database.CreateTable<Users>();
        }
        public async System.Threading.Tasks.Task CreateTableCategory()
        {
            await database.CreateTableAsync<Category>();
        }
        public async System.Threading.Tasks.Task CreateTableTask()
        {
            await database.CreateTableAsync<Task>();
        }
        public async System.Threading.Tasks.Task CreateTableUsers()
        {
            await database.CreateTableAsync<Users>();
        }

        public async Task<IEnumerable<Category>> GetItemsCategory()
        {
            return await database.Table<Category>().ToListAsync();
        }
        public async Task<List<ToDoList.Task>> GetItemsTask()
        {
            return await database.Table<Task>().ToListAsync();
        }
        public async Task<List<Users>> GetItemsUsers()
        {
            return await database.Table<Users>().ToListAsync();
        }
        public async Task<Category> GetItemCategory(int id)
        {
            return await database.GetAsync<Category>(id);
        }
        public async System.Threading.Tasks.Task<Task> GetItemTask(int id)
        {
            return await database.GetAsync<Task>(id);
        }
        public async Task<int> DeleteItemCategory(int id)
        {
            return await database.DeleteAsync<Category>(id);
        }
        public async Task<int> DeleteItemTask(int id)
        {
            return await database.DeleteAsync<Task>(id);
        }
        public async Task<int> SaveItemCategory(Category category)
        {
            if (category.IdCategory != 0)
            {
                await database.UpdateAsync(category);
                return category.IdCategory;
            }
            else
            {
                return await database.InsertAsync(category);
            }
        }
        public async Task<int> SaveItemTask(Task task)
        {
            if (task.IdTask != 0)
            {
                 return await database.InsertAsync(task);
                
            }
            else
            {
                return await database.InsertAsync(task);
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
                await database.InsertAsync(user);
            }
           
        }
        public async Task<Users> GetItemUser(string email)
        {
            try
            {
                return await database.GetAsync<Users>(email);
            }
            catch (Exception e)
            {
                string ex = e.StackTrace.ToString();
                return null;
            }
        }
    }
}
