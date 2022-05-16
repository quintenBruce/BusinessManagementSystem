using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels
{
    public class OrderViewModel
    {
        public Order order { get; set; }
        public Customer customer { get; set; }
        public PaymentHistory paymentHistory { get; set; }
        public List<Product> products { get; set; }
        public List<Category> categories { get; set; }
        public List<int> categoryIds { get; set; }
        
    }
}
