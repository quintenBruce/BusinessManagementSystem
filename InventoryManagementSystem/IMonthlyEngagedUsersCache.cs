namespace InventoryManagementSystem
{
    public interface IMonthlyEngagedUsersCache
    {
        void CacheData(string key, int value, DateTime expirationDate);
        int GetChachedData(string key);
    }
}