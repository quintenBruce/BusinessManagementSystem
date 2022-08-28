namespace InventoryManagementSystem.Models
{
    public class OrderGroup
    {
        public Order order { get; set; }
        public List<Product> products { get; set; }
        public List<Payment> paymentHistory { get; set; }
    }
}