using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class PaymentHistory
    {
        public int Id { get; set; }

        [Display(Name = "Down Payment Amount")]
        public float PaymentAmount { get; set; }

        [Display(Name = "Payment Service")]
        public string PaymentType { get; set; }

        public Order Order { get; set; }
    }
}