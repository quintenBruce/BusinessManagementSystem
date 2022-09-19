using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Display(Name = "Down Payment Amount")]
        public float Amount { get; set; }

        [Display(Name = "Payment Service")]
        public string Type { get; set; }
        

        public Order? Order { get; set; }
    }
}