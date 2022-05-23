using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels
{
    public class OrderRetrievalModel
    {
        public List<Order> allOrders { get; set; }
        public List<Customer> allCustomers { get; set; }
        public List<PaymentHistory> allPaymentHistories { get; set; }
        public List<Product> allProducts { get; set; }

        
    }
}
