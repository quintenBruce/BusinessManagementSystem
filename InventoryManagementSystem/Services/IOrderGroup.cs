using InventoryManagementSystem.Models;
namespace InventoryManagementSystem.Services
{
    public interface IOrderGroup
    {
        bool CreateOrderGroup(List<Product> products, Order order, Payment payment, Customer customer);
        OrderGroup GetOrderGroup(int Id);
    }
}
