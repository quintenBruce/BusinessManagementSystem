using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Services
{
    public class ProductService : IProduct
    {
        public bool CreateProduct(Product product, int orderId)
        {

            using (OrdersContext context = new OrdersContext())
            {
                string categoryName = product.Category.Name;
                product.Category = null;
                
                context.Products.Add(product);
                
                
                product.Order = context.Orders.Where(order => order.Id == orderId).First();
                product.Category = context.Categories.Where(category => category.Name.Trim() == categoryName).First();
                
               
               
                var status = 0;
                status = context.SaveChanges();
                if (status != 0)
                {
                    context.Update(product);
                    product.Order.Balance += product.Price;
                    product.Order.Total += product.Price;

                    var status2 = context.SaveChanges();

                }

                return status == 0 ? false : true;
            }
        }

        public bool DeleteProduct(int productId)
        {
            using (OrdersContext context = new OrdersContext())
            {

                var product = context.Products.Include(product => product.Order).Where(product => product.Id == productId).First();

                context.Products.Remove(product);
                product.Order.Balance = product.Order.Balance - product.Price;
                product.Order.Total = product.Order.Total - product.Price;
                
                var status = context.SaveChanges();
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

        public bool UpdateProduct(int existingProductId, Product updatedProduct)
        {
            using (OrdersContext context = new OrdersContext())
            {
                var existingProduct = context.Products.AsNoTracking().Include(product => product.Category).First(_product => _product.Id == existingProductId);
                
                int orderId = updatedProduct.Order.Id;
                int existingCategory = existingProduct.Category.Id;
                string existingCategoryName = existingProduct.Category.Name;
                string updatedCategoryName = updatedProduct.Category.Name;


                
                updatedProduct.Category.Name = null;

                existingProduct.Category = null;
                updatedProduct.Category = null;
                updatedProduct.Order = null;

            
                
                
                foreach (var property in existingProduct.GetType().GetProperties().Where(property => property.PropertyType == typeof(string) && property.GetValue(existingProduct) != null))
                    property.SetValue(existingProduct, property.GetValue(existingProduct).ToString().Trim());

                foreach (var property in updatedProduct.GetType().GetProperties().Where(property => property.PropertyType == typeof(string) && property.GetValue(updatedProduct) != null))
                    property.SetValue(updatedProduct, property.GetValue(updatedProduct).ToString().Trim());

                existingCategoryName = existingCategoryName != null ? existingCategoryName.Trim() : "";
                updatedCategoryName = updatedCategoryName.Trim();




                bool equality = JsonConvert.SerializeObject(existingProduct) == JsonConvert.SerializeObject(updatedProduct) && JsonConvert.SerializeObject(existingCategoryName) == JsonConvert.SerializeObject(updatedCategoryName);


                if (equality)
                {
                    context.ChangeTracker.Clear();
                    return false;
                }
                    
                else
                {
                    existingProduct = updatedProduct;
                    existingProduct.Category = context.Categories.AsNoTracking().Where(category => category.Name == updatedCategoryName).First();
                    existingProduct.Order = context.Orders.First(order => order.Id == orderId);
                    context.Update(existingProduct);
                    var ylksdf = context.SaveChanges() == 1 ? true : false;

                    return true;
                }


            }


                
        }
    }
}
