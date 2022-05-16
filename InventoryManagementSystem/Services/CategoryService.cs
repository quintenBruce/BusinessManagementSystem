using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class CategoryService : ICategory
    {
        public bool CreateCategory(Category category)
        {
            using (OrdersContext context = new OrdersContext())
            {
                context.Categories.Add(category);
                var status = context.SaveChanges();
                return status == 0 ? false : true;
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            using (OrdersContext context = new OrdersContext())
            {
                var categories = new List<Category>();
                var retrievedCategories = await context.Categories.AsNoTracking().ToListAsync();
                foreach (var category in retrievedCategories)
                {
                    categories.Add(category);
                }
                
                return categories;

            }
        }
    }
}
