using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin_test.Models;
using SQLite;

namespace Xamarin_test.Services
{
    public class MockDataStore : IDataStore<Mission> // Задачи
    {
        SQLiteAsyncConnection database;
        readonly List<Mission> items;

        public MockDataStore()
        {
            //добавление данных из БД
            string databasePath = "";
            database = new SQLiteAsyncConnection(databasePath);          
            items = new List<Mission>();
        }

        public async Task CreateTable()
        {
            await database.CreateTableAsync<Mission>();
        }

        public async Task<int> SaveItemAsync(Mission item)
        {
            //Добавление данных в БД
            if (item.Id != 0)
            {
                return await database.UpdateAsync(item);
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        public async Task<bool> DeleteItemAsync(int id)
        {
            //удаление данных БД
            database.DeleteAsync(id);

            return await Task.FromResult(true);
        }

        public async Task<Mission> GetItemAsync(int id)
        {
            // возвращает 1 объект из БД
            return await database.GetAsync<Mission>(id);
        }

        public async Task<IEnumerable<Mission>> GetItemsAsync(bool forceRefresh = false)
        {
            // возвращает все объекты таблицы БД
            return await database.Table<Mission>().ToListAsync();
        }
    }

    public class MockDataStoreDaily : IDataStore<Daily> // Ежедневные задачи
    {
        SQLiteConnection database;
        readonly List<Daily> items;

        public MockDataStoreDaily()
        {
            //добавление данных из БД
            string databasePath = "";
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Mission>();
            items = new List<Daily>()
            {
                new Daily { Id = 0, Text = "First item", Description="This is an item description.",}
            };
        }

        public async Task<int> SaveItemAsync(Daily item)
        {
            //Добавление данных в БД
            if (item.Id != 0)
            {
                database.Update(item);
                return await Task.FromResult(item.Id);
            }
            else
            {
                return await Task.FromResult(database.Insert(item)); ;
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            //удаление данных БД
            var oldItem = items.Where((Daily arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Daily> GetItemAsync(int id)
        {
            // возвращает 1 объект из БД
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Daily>> GetItemsAsync(bool forceRefresh = false)
        {
            // возвращает все объекты таблицы БД
            return await Task.FromResult(items);
        }
    }

    public class MockDataStorePurpose : IDataStore<Purpose> // Цели
    {
        SQLiteConnection database;
        readonly List<Purpose> items;

        public MockDataStorePurpose()
        {
            //добавление данных из БД
            string databasePath = "";
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Mission>();
            items = new List<Purpose>()
            {
                new Purpose { Id = 0, Text = "First item", Description="This is an item description.",}
            };
        }

        public async Task<int> SaveItemAsync(Purpose item)
        {
            //Добавление данных в БД
            if (item.Id != 0)
            {
                database.Update(item);
                return await Task.FromResult(item.Id);
            }
            else
            {
                return await Task.FromResult(database.Insert(item)); ;
            }
        }

        public async Task<bool> UpdateItemAsync(Purpose item)
        {
            //Изменение данных БД
            var oldItem = items.Where((Purpose arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            //удаление данных БД
            var oldItem = items.Where((Purpose arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Purpose> GetItemAsync(int id)
        {
            // возвращает 1 объект из БД
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Purpose>> GetItemsAsync(bool forceRefresh = false)
        {
            // возвращает все объекты таблицы БД
            return await Task.FromResult(items);
        }
    }

    public class MockDataStoreDay : IDataStore<Day>
    {
        SQLiteConnection database;
        readonly List<Day> items;

        public MockDataStoreDay()
        {
            //добавление данных из БД
            string databasePath = "";
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Mission>();
            items = new List<Day>()
            {
                new Day { Id = 0, day = new DateTime(2014, 5, 8), dailies = null, dayOfTheWeek = DayOfWeek.Sunday}
            };
        }

        public async Task<int> SaveItemAsync(Day item)
        {
            //Добавление данных в БД
            if (item.Id != 0)
            {
                database.Update(item);
                return await Task.FromResult(item.Id);
            }
            else
            {
                return await Task.FromResult(database.Insert(item)); ;
            }
        }

        public async Task<bool> UpdateItemAsync(Day item)
        {
            //Изменение данных БД
            var oldItem = items.Where((Day arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            //удаление данных БД
            var oldItem = items.Where((Day arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Day> GetItemAsync(int id)
        {
            // возвращает 1 объект из БД
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Day>> GetItemsAsync(bool forceRefresh = false)
        {
            // возвращает все объекты таблицы БД
            return await Task.FromResult(items);
        }
    }
}