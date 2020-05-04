using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PourItOut.Models;

namespace PourItOut.Services
{
    public class MockDataStore : IDataStore<Player>
    {
        readonly List<Player> items;

        public MockDataStore()
        {
            items = new List<Player>()
            {
                new Player { Id = Guid.NewGuid().ToString(), Name = "First item" },
                new Player { Id = Guid.NewGuid().ToString(), Name = "Second item" },
                new Player { Id = Guid.NewGuid().ToString(), Name = "Third item" }
            };
        }

        public async Task<bool> AddItemAsync(Player item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Player item)
        {
            var oldItem = items.Where((Player arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Player arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Player> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Player>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}