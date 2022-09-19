using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }

        [Display(Name = "Total")]
        public float Total { get; set; }
        [Display(Name = "Balance")]
        public float Balance { get; set; }
        [Display(Name = "Placement Date")]
        public DateTime PlacementDate { get; set; }

        [Display(Name = "Est. Fulfillment Date")]
        [Required]
        public DateTime FulfillmentDate { get; set; }
        [Display(Name = "Completion Date")]

        public DateTime? CompletionDate { get; set; }
        [Display(Name = "Communication Thread")]
        [Required]
        public string ComThread { get; set; }
        [Display(Name = "Status")]

        
        public bool Status { get; set; }

        [Display(Name = "Delivery Fee")]
        public int? DeliveryFee { get; set; }

        [Display(Name = "Out-of-Town")]
        public bool OutOfTown { get; set; }

        public ICollection<Product>? Products { get; set; }
        public ICollection<Payment>? Payments { get; set; }

        public static implicit operator List<object>(Order v)
        {
            throw new NotImplementedException();
        }
    }
}