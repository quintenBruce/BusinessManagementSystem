using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface IOrder
    {
        bool CreateOrder(Order order);

    }
}
