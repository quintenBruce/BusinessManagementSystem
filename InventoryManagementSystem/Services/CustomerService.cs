using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class CustomerService : ICustomer
    {
        public bool CreateCustomer(Customer customer)
        {
            using (OrdersContext context = new OrdersContext())
            {
               
                context.Customers.Add(customer);
                var status = 0;
                return status == 0 ? false : true;

            }
        }
    }
}
