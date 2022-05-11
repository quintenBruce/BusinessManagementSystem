namespace InventoryManagementSystem.Models
{
    public class PaymentHistory
    {
        public int Id { get; set; }
        public float PaymentAmount { get; set; }
        public string PaymentType { get; set; }
        public Order Order { get; set; }

    }
}
