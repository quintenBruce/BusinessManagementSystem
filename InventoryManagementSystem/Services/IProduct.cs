using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface IProduct
    {
        bool CreateProduct(Product product);
        Task<List<Product>> GetAllProducts();
    }
}
