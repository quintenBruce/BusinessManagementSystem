using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public class OrderService : IOrder
    {
        public bool CompleteOrder(int Id)
        {
            using (OrdersContext context = new OrdersContext())
            {
                context.Orders.Where(order => order.Id == Id).ToList().FirstOrDefault().Order_status = true;
                context.Orders.Where(order => order.Id == Id).ToList().FirstOrDefault().Order_completion_date = DateTime.Now;

                var status = context.SaveChanges();
                return status == 0 ? false : true;
            }
        }

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
                var order = context.Orders.Where(order => order.Id == Id).First();
                context.Orders.Remove(order);

                var status = context.SaveChanges();
                return status == 0 ? false : true;
            }
        }
    }
}