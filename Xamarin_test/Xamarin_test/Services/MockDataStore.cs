using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin_test.Models;

namespace Xamarin_test.Services
{
    public class MockDataStore : IDataStore<Mission>
    {
        readonly List<Mission> items;

        public MockDataStore()
        {
            items = new List<Mission>()
            {
                new Mission { Id = 0, Text = "First item", Description="This is an item description.",}
            };
        }

        public async Task<bool> AddItemAsync(Mission item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Mission item)
        {
            var oldItem = items.Where((Mission arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Mission arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Mission> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Mission>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}