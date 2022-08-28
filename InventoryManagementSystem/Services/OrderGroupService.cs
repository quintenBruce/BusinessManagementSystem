using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class OrderGroupService : IOrderGroup
    {
        private readonly OrdersContext _context;

        public OrderGroupService(OrdersContext context)
        {
            _context = context;
        }

        public bool CreateOrderGroup(List<Product> products, Order order, Payment payment, Customer customer)
        {
            _context.Customers.Add(customer);
            var status = _context.SaveChanges();
            order.Customer = customer;
            _context.Orders.Add(order);
            status = _context.SaveChanges();
            if (payment.Amount != 0 && payment.Type != "Select a payment type")
            {
                _context.Payments.Add(payment);
                status = _context.SaveChanges();
            }

            for (int i = 0; i < products.Count(); i++)
            {
                products[i].Category = _context.Categories.First(category => category.Id == products[i].Category.Id);
                _context.Add(products[i]);
                _context.Entry(products[i].Category).State = EntityState.Detached;
            }
            status = _context.SaveChanges();

            return status == 0 ? false : true;
        }

        public OrderGroup GetOrderGroup(int Id)
        {
            OrderGroup orderGroup = new OrderGroup();

            orderGroup.order = _context.Orders.Include(order => order.Customer).Where(order => order.Id == Id).First();
            orderGroup.products = _context.Products.Include(product => product.Order).Include(product => product.Category).Where(product => product.Order == orderGroup.order).ToList();
            orderGroup.paymentHistory = _context.Payments.Where(payment => payment.Order == orderGroup.order).ToList();

            return orderGroup;
        }
    }
}