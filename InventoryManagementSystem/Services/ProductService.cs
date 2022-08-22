using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Services
{
    public class ProductService : IProduct
    {
        private readonly OrdersContext _ordersContext;

        public ProductService(OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
        }

        public bool CreateProduct(Product product, int orderId)
        {
            string categoryName = product.Category.Name;
            product.Category = null;

            _ordersContext.Products.Add(product);

            product.Order = _ordersContext.Orders.Where(order => order.Id == orderId).First();
            product.Category = _ordersContext.Categories.Where(category => category.Name.Trim() == categoryName).First();

            var status = 0;
            status = _ordersContext.SaveChanges();
            if (status != 0)
            {
                _ordersContext.Update(product);
                product.Order.Balance += product.Price;
                product.Order.Total += product.Price;

                var status2 = _ordersContext.SaveChanges();
            }

            return status == 0 ? false : true;
        }

        public bool DeleteProduct(int productId)
        {
            var product = _ordersContext.Products.Include(product => product.Order).Where(product => product.Id == productId).First();

            _ordersContext.Products.Remove(product);
            product.Order.Balance = product.Order.Balance - product.Price;
            product.Order.Total = product.Order.Total - product.Price;

            var status = _ordersContext.SaveChanges();
            return status == 0 ? false : true;
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            var retrievedProducts = _ordersContext.Products.Include(x => x.Order).AsNoTracking().ToList();
            foreach (var product in retrievedProducts)
            {
                products.Add(product);
            }

            return products;
        }

        

        public bool UpdateProduct(int existingProductId, Product updatedProduct)
        {
            var existingProduct = _ordersContext.Products.AsNoTracking().Include(product => product.Category).First(_product => _product.Id == existingProductId);

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
                _ordersContext.ChangeTracker.Clear();
                return false;
            }
            else
            {
                existingProduct = updatedProduct;
                existingProduct.Category = _ordersContext.Categories.AsNoTracking().Where(category => category.Name == updatedCategoryName).First();
                existingProduct.Order = _ordersContext.Orders.First(order => order.Id == orderId);
                _ordersContext.Update(existingProduct);
                var ylksdf = _ordersContext.SaveChanges() == 1 ? true : false;

                return true;
            }
        }
    }
}