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
        public DateTime Order_date { get; set; }
        [Display(Name = "Order Fulfillment Date")]
        public DateTime Order_fulfillment_date { get; set; }
        [Display(Name = "Communication Thread")]
        public string Com_thread { get; set; }
        [Display(Name = "Order Status")]
        public bool Order_status { get; set; }
        [Display(Name = "Delivery?")]
        public bool Delivery { get; set; }
        [Display(Name = "Out-of-Town?")]
        public bool Out_Of_Town { get; set; }

    }
}
