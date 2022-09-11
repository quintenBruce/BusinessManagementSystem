using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public string Type { get; set; } = null!;

        public static PaymentDTO ToPaymentDTO(Payment payment)
        {
            return new PaymentDTO
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Type = payment.Type,
            };
        }
    }
}