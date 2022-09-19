using InventoryManagementSystem.Models;
using InventoryManagementSystem.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace InventoryManagementSystem.ViewModels
{
    public class CreateOrderViewModel
    {
        [Display(Name = "Est. Fulfillment Date")]
        [Required(ErrorMessage = "Est. fulfillment date is required.")]
        public DateTime? FulfillmentDate { get; set; } = DateTime.Now;
        
        [Display(Name = "Down Payment Amount")]
        public float? DownPaymentAmount { get; set; }
        [Display(Name = "Payment Service")]
        public string DownPaymentType { get; set; }
        [Display(Name = "Delivery Fee")]
        public int? DeliveryFee { get; set; }
        [Required(ErrorMessage = "Customer name is requrired")]
        public string Name { get; set; }
        [Display(Name = "Out-Of-Town")]
        public bool OutOfTown { get; set; }
        [Display(Name = "Communication Thread")]
        [Required(ErrorMessage = "Communication thread is required")]
        public string ComThread { get; set; }
        [Display(Name = "Phone Number")]
        public int? PhoneNumber { get; set; }
        public ICollection<Product>? Products { get; set; }
        public static Order ToOrder(CreateOrderViewModel m)
        {
            return new Order
            {
                FulfillmentDate = m.FulfillmentDate ?? DateTime.Now,
                Products = m.Products,
                Payments = new List<Payment> { new Payment { Amount = m.DownPaymentAmount ?? 0, Type = m.DownPaymentType } },
                DeliveryFee = m.DeliveryFee,
                Customer = new Customer { FullName = m.Name, PhoneNumber = m.PhoneNumber },
                OutOfTown = m.OutOfTown,
                ComThread = m.ComThread

            };
        }


    }
}
