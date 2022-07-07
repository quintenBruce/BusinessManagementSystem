using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels
{
    public class OrderCustomerDetailsViewModel
    {
        public Order order { get; set; }
        public Customer customer { get; set; }

        public OrderCustomerDetailsViewModel(Order order)
        {
            this.order = order;
            this.order.Customer = customer;
        }
    }
}
