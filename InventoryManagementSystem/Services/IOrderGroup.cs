using InventoryManagementSystem.Models;
namespace InventoryManagementSystem.Services
{
    public interface IOrderGroup
    {
        bool CreateOrderGroup(List<Product> products, Order order, PaymentHistory payment, Customer customer, List<int> categoryIds);
    }
}
