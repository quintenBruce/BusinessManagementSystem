using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class CategoryService : ICategory
    {
        private readonly OrdersContext _context;

        public CategoryService(OrdersContext context)
        {
            _context = context;
        }

        public bool CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            var status = _context.SaveChanges();
            return status == 0 ? false : true;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = new List<Category>();
            var retrievedCategories = await _context.Categories.AsNoTracking().ToListAsync();
            foreach (var category in retrievedCategories)
                categories.Add(category);
            return categories;
        }
    }
}