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

        public bool DeleteOrder(int Id)
        {
            using (OrdersContext context = new OrdersContext())
            {
                var order = context.Orders.Where(order =>order.Id == Id).First();
                context.Orders.Remove(order);

                var status = context.SaveChanges();
                return status == 0 ? false : true;
            }
        }
    }
}
