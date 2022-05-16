using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface ICategory
    {
        bool CreateCategory(Category category);
        Task<List<Category>> GetAllCategories();
    }
}
