using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface IProduct
    {
        bool CreateProduct(Product product, int orderId);
        Task<List<Product>> GetAllProducts();
        bool DeleteProduct(int productId);
        bool UpdateProduct(int existingProduct, Product updatedProduct);
    }
}
