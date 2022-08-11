using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class OrderGroupService : IOrderGroup
    {
        public bool CreateOrderGroup(List<Product> products, Order order, PaymentHistory payment, Customer customer)
        {
            using (OrdersContext context = new OrdersContext())
            {
                context.Customers.Add(customer);
                var status = context.SaveChanges();
                order.Customer = customer;
                context.Orders.Add(order);
                status = context.SaveChanges();
                if (payment.PaymentAmount != 0 && payment.PaymentType != "Select a payment type")
                {
                    context.PaymentHistories.Add(payment);
                    status = context.SaveChanges();
                }


                for (int i = 0; i < products.Count(); i++)
                {
                    products[i].Category = context.Categories.First(category => category.Id == products[i].Category.Id);
                    context.Add(products[i]);
                    context.Entry(products[i].Category).State = EntityState.Detached;

                }
                status = context.SaveChanges();

                return status == 0 ? false : true;

            }
        }

        public OrderGroup GetOrderGroup(int Id)
        {
            OrderGroup orderGroup = new OrderGroup();
            using (OrdersContext context = new OrdersContext())
            {
                orderGroup.order = context.Orders.Include(order => order.Customer).Where(order => order.Id == Id).First();
                orderGroup.products = context.Products.Include(product => product.Order).Include(product => product.Category).Where(product => product.Order == orderGroup.order).ToList();


                
                orderGroup.paymentHistory = context.PaymentHistories.Where(payment => payment.Order == orderGroup.order).ToList();
            }

            return orderGroup;      
        }
    }
}
