using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Todo
{
    public class TodoItemDatabase
    {
        readonly SQLiteAsyncConnection database;

        public TodoItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TodoItem>().Wait();
            //.CreateTableAsync<ObjectGroup>().Wait();
            //database.CreateTableAsync<Object>().Wait();
            //database.CreateTableAsync<TodoObject>().Wait();
        }

        #region TodoItem
        public Task<List<TodoItem>> GetItemsAsync()
        {
            return database.Table<TodoItem>().ToListAsync();
        }

        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            return database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return database.DeleteAsync(item);
        }
        #endregion TodoItem
        
        #region ObjectGroup
        //Считать все группы для подбора максимально соответствующей
        public Task<List<ObjectGroup>> GetObjectGroupsAsync()
        {
            return database.Table<ObjectGroup>().ToListAsync();
        }

        #endregion ObjectGroup

        #region Objects
        //Добавить в таблицу список объект для группы
        public Task<int> SaveObjectAsync(Obj obj)
        {
            if (obj.ID != 0)
            {
                return database.UpdateAsync(obj);
            }
            else
            {
                return database.InsertAsync(obj);
            }
        }

        //Добавить в таблицу список объектов для группы
        public void SaveObjects(List<Obj> objs)
        {
            foreach (Obj obj in objs)
                SaveObjectAsync(obj);
        }

        //Удалить из таблицы список объектов для группы		
        public Task<int> DeleteObjectAsync(Obj obj)
        {
            return database.DeleteAsync(obj);
        }
        #endregion Objects

        #region TodoObjects
        //Добавить связку объекта с группой
        public Task<int> SaveTodoObjectsAsync(Obj obj)
        {
            if (obj.ID != 0)
            {
                return database.UpdateAsync(obj);
            }
            else
            {
                return database.InsertAsync(obj);
            }
        }

        //Получить все TodoItemId для соответствующие группе
        public Task<List<TodoItem>> GetItemsByGroupAsync(int groupObjectID)
        {        
            return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] JOIN [TodoObjects] ON [TodoItem].[ID] = [TodoObjects].[TodoItemID] WHERE  [Done] = 0 AND [TodoObjects].[ObjectGroupID] = " + groupObjectID.ToString());
        }

        //Удалить связку объекта с группой
        public Task<int> DeleteTodoObjectAsync(TodoObject todoObj)
        {
            return database.DeleteAsync(todoObj);
        }
        #endregion TodoObjects
        
    }
}

