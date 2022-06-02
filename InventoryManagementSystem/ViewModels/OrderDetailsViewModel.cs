using InventoryManagementSystem.Models;
namespace InventoryManagementSystem.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Order order { get; set; }
        public Customer customer { get; set; }
        public List<PaymentHistory>? paymentHistory { get; set; }
        public List<Category> category { get; set; }
        public List<Product> products { get; set; }

        public OrderDetailsViewModel()
        {

        }
        public OrderDetailsViewModel(Order _order, Customer _customer, List<PaymentHistory> _payments, List<Category> _category, List<Product> _products)
        {
            this.order = _order;
            this.customer = _customer;
            this.paymentHistory = _payments;
            this.category = _category;
            this.products = _products;
        }
    }
}
