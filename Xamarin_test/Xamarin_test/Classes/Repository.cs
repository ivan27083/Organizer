using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin_test.Models;

namespace Xamarin_test.Classes
{
    public class MissionRepository
    {
        SQLiteConnection database;
        public MissionRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Mission>();
        }
        public IEnumerable<Mission> GetItems()
        {
            return database.Table<Mission>().ToList();
        }
        public Mission GetItem(int id)
        {
            return database.Get<Mission>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Mission>(id);
        }
        public int SaveItem(Mission item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
