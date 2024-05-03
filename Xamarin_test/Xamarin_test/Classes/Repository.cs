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

    public class DailyRepository
    {
        SQLiteConnection database;
        public DailyRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Daily>();
        }
        public IEnumerable<Daily> GetItems()
        {
            return database.Table<Daily>().ToList();
        }
        public Daily GetItem(int id)
        {
            return database.Get<Daily>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Daily>(id);
        }
        public int SaveItem(Daily item)
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

    public class PurposeRepository
    {
        SQLiteConnection database;
        public PurposeRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Purpose>();
        }
        public IEnumerable<Purpose> GetItems()
        {
            return database.Table<Purpose>().ToList();
        }
        public Purpose GetItem(int id)
        {
            return database.Get<Purpose>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Purpose>(id);
        }
        public int SaveItem(Purpose item)
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

    public class DayRepository
    {
        SQLiteConnection database;
        public DayRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Day>();
        }
        public IEnumerable<Day> GetItems()
        {
            return database.Table<Day>().ToList();
        }
        public Day GetItem(int id)
        {
            return database.Get<Day>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Day>(id);
        }
        public int SaveItem(Day item)
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
