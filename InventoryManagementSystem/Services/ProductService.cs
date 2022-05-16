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
    }
}
