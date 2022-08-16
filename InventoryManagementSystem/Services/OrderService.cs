using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

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

        public bool DeleteOrder(int orderId)
        {
            using (OrdersContext context = new OrdersContext())
            {
                context.Remove(context.Orders.First(order => order.Id == orderId));
                context.RemoveRange(context.Products.Where(product => product.Order.Id == orderId).ToList());

                if (context.PaymentHistories.Where(payment => payment.Order.Id == orderId).ToList() != null)
                {
                    var payments = context.PaymentHistories.Where(payment => payment.Order.Id == orderId).ToList();
                    context.RemoveRange(payments);
                }
                    

                var status = context.SaveChanges();
                return status == 0 ? false : true;
            }
        }

        public static bool OrderExists(int orderId)
        {
            using (OrdersContext context = new OrdersContext())
            {
                return context.Orders.Any(order => order.Id == orderId);
            }
        }
    }
}