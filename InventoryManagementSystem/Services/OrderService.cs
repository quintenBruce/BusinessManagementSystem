using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public class OrderService : IOrder
    {
        private readonly OrdersContext context;

        public OrderService(OrdersContext ordersContext)
        {
            context = ordersContext;
        }

        //COMPLETED

        public bool CompleteOrder(int Id) 
        {
            context.Orders.Where(order => order.Id == Id).ToList().FirstOrDefault().Status = true;
            context.Orders.Where(order => order.Id == Id).ToList().FirstOrDefault().CompletionDate = DateTime.Now;

            var status = context.SaveChanges();
            return status == 0 ? false : true;
        }

        public bool CreateOrder(Order order)
        {
            order.Customer.Id = 0;
            context.Orders.Add(order);

            var status = context.SaveChanges();
            return status == 0 ? false : true;
        }

        public bool DeleteOrder(int orderId)
        {
            context.Remove(context.Orders.First(order => order.Id == orderId));
            context.RemoveRange(context.Products.Where(product => product.Order.Id == orderId).ToList());

            if (context.Payments.Where(payment => payment.Order.Id == orderId).ToList() != null)
            {
                var payments = context.Payments.Where(payment => payment.Order.Id == orderId).ToList();
                context.RemoveRange(payments);
            }

            var status = context.SaveChanges();
            return status == 0 ? false : true;
        }

        public bool OrderExists(int orderId)
        {
            return context.Orders.Any(order => order.Id == orderId);
        }

        public List<Order> GetOrders()
        {
            return context.Orders.ToList();
        }
    }
}