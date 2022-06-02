namespace InventoryManagementSystem.Models
{
    public class OrderGroup
    {
        public Order order { get; set; }
        public List<Product> products { get; set; }
        public List<PaymentHistory> paymentHistory { get; set; }
        public Customer customer { get; set; }
        
    }
}
