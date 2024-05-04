using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin_test.Classes;
using Xamarin_test.Models;
using Xamarin_test.Services;

namespace Xamarin_test.Services
{
    public class MockDataStore : IDataStore<Mission> // Задачи
    {
        readonly List<Mission> items;
        public MockDataStore()
        {
            //добавление данных из БД
            items = new List<Mission>();
            using (ApplicationContext db = new ApplicationContext())
            {
                items = db.missions.ToList();
            }
        }
        public async Task<int> AddItemAsync(Mission item)
        {
            //Добавление данных в БД
            using (ApplicationContext db = new ApplicationContext())
            {
                db.missions.Add(item);
                await Task.FromResult(db.SaveChanges());
            }
            return 0;
        }
        public async Task<int> DeleteItemAsync(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var item = db.missions.Find(id);
                db.missions.Remove(item);
                await Task.FromResult(db.SaveChanges());
                return 0;
            }
        }
        public async Task<Mission> GetItemAsync(int id)
        {
            // возвращает 1 объект из БД
            using (ApplicationContext db = new ApplicationContext())
            {
                return await Task.FromResult(db.missions.Find(id));
            }
        }

        public async Task<IEnumerable<Mission>> GetItemsAsync(bool forceRefresh = false)
        {
            // возвращает все объекты таблицы БД
            using (ApplicationContext db = new ApplicationContext())
            {
                return await Task.FromResult(db.missions.ToList());
            }
        }
        public async Task<int> UpdateItemAsync(Mission item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.missions.Update(item);
                return await Task.FromResult(db.SaveChanges());
            }
        }
    }

    public class MockDataStoreDaily : IDataStore<Daily> // Задачи
    {
        readonly List<Daily> items;

        public MockDataStoreDaily()
        {
            //добавление данных из БД
            items = new List<Daily>();
            using (ApplicationContext db = new ApplicationContext())
            {
                items = db.dailies.ToList();
            }
        }

        public async Task<int> AddItemAsync(Daily item)
        {
            //Добавление данных в БД
            using (ApplicationContext db = new ApplicationContext())
            {
                db.dailies.Add(item);
                await Task.FromResult(db.SaveChanges());
            }
            return 0;
        }
        public async Task<int> DeleteItemAsync(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var item = db.dailies.Find(id);
                db.dailies.Remove(item);
                await Task.FromResult(db.SaveChanges());
                return 0;
            }
        }
        public async Task<Daily> GetItemAsync(int id)
        {
            // возвращает 1 объект из БД
            using (ApplicationContext db = new ApplicationContext())
            {
                return await Task.FromResult(db.dailies.Find(id));
            }
        }

        public async Task<IEnumerable<Daily>> GetItemsAsync(bool forceRefresh = false)
        {
            // возвращает все объекты таблицы БД
            using (ApplicationContext db = new ApplicationContext())
            {
                return await Task.FromResult(db.dailies.ToList());
            }
        }
        public async Task<int> UpdateItemAsync(Daily item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.dailies.Update(item);
                return await Task.FromResult(db.SaveChanges());
            }
        }
    }


    public class MockDataStorePurpose : IDataStore<Purpose> // Задачи
    {
        readonly List<Purpose> items;

        public MockDataStorePurpose()
        {
            //добавление данных из БД
            items = new List<Purpose>();
            using (ApplicationContext db = new ApplicationContext())
            {
                items = db.purposes.ToList();
            }
        }

        public async Task<int> AddItemAsync(Purpose item)
        {
            //Добавление данных в БД
            using (ApplicationContext db = new ApplicationContext())
            {
                db.purposes.Add(item);
                await Task.FromResult(db.SaveChanges());
            }
            return 0;
        }
        public async Task<int> DeleteItemAsync(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var item = db.purposes.Find(id);
                db.purposes.Remove(item);
                await Task.FromResult(db.SaveChanges());
                return 0;
            }
        }
        public async Task<Purpose> GetItemAsync(int id)
        {
            // возвращает 1 объект из БД
            using (ApplicationContext db = new ApplicationContext())
            {
                return await Task.FromResult(db.purposes.Find(id));
            }
        }

        public async Task<IEnumerable<Purpose>> GetItemsAsync(bool forceRefresh = false)
        {
            // возвращает все объекты таблицы БД
            using (ApplicationContext db = new ApplicationContext())
            {
                return await Task.FromResult(db.purposes.ToList());
            }
        }
        public async Task<int> UpdateItemAsync(Purpose item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.purposes.Update(item);
                return await Task.FromResult(db.SaveChanges());
            }
        }
    }
    public class MockDataStoreDay : IDataStore<Day> // Задачи
    {
        readonly List<Day> items;
        public MockDataStoreDay()
        {
            //добавление данных из БД
            using (ApplicationContext db = new ApplicationContext())
            {
                items = db.days.ToList();
                foreach(Day day in items)
                {
                    var items = db.dailies.Include(p => p.Day).Where(u => u.Day == day.dayOfTheWeek);
                    foreach (var item in items)
                    {
                        day.dailies.Add(item);
                    }
                }
            }
        }
        public async Task<int> AddItemAsync(Day item)
        {
            //Добавление данных в БД
            using (ApplicationContext db = new ApplicationContext())
            {
                db.days.Add(item);
                await Task.FromResult(db.SaveChanges());
            }
            return 0;
        }
        public async Task<int> DeleteItemAsync(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var item = db.days.Find(id);
                db.days.Remove(item);
                await Task.FromResult(db.SaveChanges());
                return 0;
            }
        }
        public async Task<Day> GetItemAsync(int id)
        {
            // возвращает 1 объект из БД
            using (ApplicationContext db = new ApplicationContext())
            {
                var day = db.days.Find(id);
                var items = db.dailies.Include(p => p.Day).Where(u => u.Day == day.dayOfTheWeek);
                foreach (var item in items)
                {
                    day.dailies.Add(item);
                }
                return await Task.FromResult(day);
            }
        }

        public async Task<IEnumerable<Day>> GetItemsAsync(bool forceRefresh = false)
        {
            // возвращает все объекты таблицы БД
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Day> _items = db.days.ToList();
                foreach (Day day in _items)
                {
                    var items = db.dailies.Include(p => p.Day).Where(u => u.Day == day.dayOfTheWeek);
                    foreach (var item in items)
                    {
                        day.dailies.Add(item);
                    }
                }
                return await Task.FromResult(_items);
            }
        }
        public async Task<int> UpdateItemAsync(Day item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.days.Update(item);
                return await Task.FromResult(db.SaveChanges());
            }
        }
    }
}