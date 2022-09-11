using Microsoft.Extensions.Caching.Memory;

namespace InventoryManagementSystem
{
    public class MonthlyEngagedUsersCache : IMonthlyEngagedUsersCache
    {
        private readonly IMemoryCache memoryCache;

        public MonthlyEngagedUsersCache(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void CacheData(string key, int value, DateTime expirationDate)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = expirationDate
            };

            memoryCache.Set<int>(key, value, options);
        }

        public int GetChachedData(string key)
        {
            var data = memoryCache.Get<int>(key);
            if (data > 0)
                return data;
            return -1;
        }
    }
}