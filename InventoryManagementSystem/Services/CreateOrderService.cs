using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public class CreateOrderService : ICreateOrder
    {
        

        public bool CreateOrder(Order order)
        {
            using (OrdersContext context = new OrdersContext())
            {
                context.Orders.Add(order);
                var status = context.SaveChanges();
                return status == 0 ? false : true;
            }
        }
    }
}
