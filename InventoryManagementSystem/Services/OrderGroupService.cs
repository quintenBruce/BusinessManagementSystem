using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public class OrderGroupService : IOrderGroup
    {
        public bool CreateOrderGroup(List<Product> products, Order order, PaymentHistory payment, Customer customer, List<int> categoryIds)
        {
            using (OrdersContext context = new OrdersContext())
            {
                context.Customers.Add(customer);
                var status = context.SaveChanges();
                order.Customer = customer;
                context.Orders.Add(order);
                status = context.SaveChanges();
                if (payment != null)
                {
                    context.PaymentHistories.Add(payment);
                    status = context.SaveChanges();
                }
                for (int i = 0; i < products.Count(); i++)
                {
                    context.Add(products[i]);

                }
                status = context.SaveChanges();

                ICategory categoryService = new CategoryService();
                var retrievedCategories = categoryService.GetAllCategories();


                for (int i = 0; i < products.Count(); i++)
                {
                    var list_ = retrievedCategories;

                    context.Products.Where(a => a.Id == products[i].Id).ToList().First().Category = retrievedCategories.Result.Where(a => a.Id == categoryIds[i]).ToList().First();

                }

                status = context.SaveChanges();

                return status == 0 ? false : true;

            }
        }
    }
}
