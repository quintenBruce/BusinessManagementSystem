using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class OrderService : IOrder
    {
        

        public bool CreateOrder(Order order)
        {
            using (OrdersContext context = new OrdersContext())
            {
                order.Customer.Id = 0;
                context.Orders.Add(order);
                
                var status = context.SaveChanges();
                return status == 0 ? false : true;
            }
        }
    }
}
