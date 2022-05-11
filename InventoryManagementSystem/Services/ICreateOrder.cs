using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface ICreateOrder
    {
        bool CreateOrder(Order order);
    }
}
