using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public class CustomerService : ICustomer
    {
        private readonly OrdersContext DBcontext;

        public CustomerService(OrdersContext ordersContext)
        {
            DBcontext = ordersContext;
        }

        public bool CreateCustomer(Customer customer)
        {
            DBcontext.Customers.Add(customer);
            var status = 0;
            return status == 0 ? false : true;
        }
    }
}