using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class ProductService : IProduct
    {
        public bool CreateProduct(Product product)
        {

            using (OrdersContext context = new OrdersContext())
            {
                
                context.Products.Add(product);

                var status = 0;
                context.SaveChanges();
                return status == 0 ? false : true;
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            using (OrdersContext context = new OrdersContext())
            {
                var products = new List<Product>();
                var retrievedProducts = await context.Products.AsNoTracking().ToListAsync();
                foreach (var product in retrievedProducts)
                {
                    products.Add(product);
                }

                return products;

            }
        }
    }
}
