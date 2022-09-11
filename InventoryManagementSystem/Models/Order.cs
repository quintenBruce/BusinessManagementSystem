using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }

        [Display(Name = "Total Order Price")]
        public float Total { get; set; }

        public float Balance { get; set; }
        public DateTime PlacementDate { get; set; }

        [Display(Name = "Order Fulfillment Date")]
        public DateTime FulfillmentDate { get; set; }

        [Display(Name = "Communication Thread")]
        public DateTime? CompletionDate { get; set; }

        public string ComThread { get; set; }

        [Display(Name = "Order Status")]
        public bool Status { get; set; }

        [Display(Name = "Delivery Fee")]
        public int DeliveryFee { get; set; }

        [Display(Name = "Out-of-Town?")]
        public bool OutOfTown { get; set; }

        public ICollection<Product>? Products { get; set; }
        public ICollection<Payment>? Payments { get; set; }

        public static implicit operator List<object>(Order v)
        {
            throw new NotImplementedException();
        }
    }
}