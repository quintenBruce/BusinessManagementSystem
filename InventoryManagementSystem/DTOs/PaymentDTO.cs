using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public float PaymentAmount { get; set; }
        public string PaymentType { get; set; } = null!;
        public static PaymentDTO ToPaymentDTO(Payment payment)
        {
            return new PaymentDTO
            { 
                Id = payment.Id,
                PaymentAmount = payment.Amount,
                PaymentType = payment.Type,
            };
        }
    }
}
