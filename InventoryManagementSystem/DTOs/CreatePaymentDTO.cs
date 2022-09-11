namespace InventoryManagementSystem.DTOs
{
    public class CreatePaymentDTO
    {
        public int Id { get; set; }

        public float Amount { get; set; }

        public string Type { get; set; }
        public int OrderId { get; set; }
    }
}