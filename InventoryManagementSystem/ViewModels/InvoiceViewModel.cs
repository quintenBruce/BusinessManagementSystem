using InventoryManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModels
{
    public class InvoiceViewModel
    {
        [DataType(DataType.Date)]
        public DateTime? PaymentDueBy { get; set; }
        public List<string>? ItemDescriptions { get; set; }
        public List<double>? ItemUnitCosts { get; set; }
        public List<int>? ItemQTYs { get; set; }
        public List<double>? ItemAmounts { get; set; }
        public List<string>? PaymentDescriptions { get; set; }
        public List<double>? PaymentUnitCosts { get; set; }
        public List<int>? PaymentQTYs { get; set; }
        public List<double>? PaymentAmounts { get; set; }
        public string? SpecialNotes { get; set; }
        public double? SubTotal { get; set; }
        public string? Discount { get; set; }
        public double? TaxRate { get; set; }
        public double? Tax { get; set; }
        public double? Total { get; set; }
        public string? CustomerInformation { get; set; }

        public InvoiceViewModel(OrderGroup orderGroup)
        {
            ItemDescriptions = new List<string>() { "", "", "", "", "", "", "", "" };
            ItemDescriptions = orderGroup.products.Select(x => (x.Name != null ? (x.Name + " | ") : "") + (x.Dimensions != null ? (x.Dimensions + " | ") : "") + (x.Description != null ? (x.Description) : "")).ToList();
            ItemUnitCosts = ItemAmounts = orderGroup.products.Select(x => (double)x.Price).ToList();

            PaymentDescriptions = orderGroup.paymentHistory.Select(x => "Payment via " +  x.PaymentType.ToString()).ToList();
            PaymentUnitCosts = PaymentAmounts = orderGroup.paymentHistory.Select(x => (double)x.PaymentAmount).ToList();

            PaymentQTYs = ItemQTYs = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1};

            string customerName = orderGroup.order.Customer.fullName;
            string customerPhoneNumber = orderGroup.order.Customer.Phone_number.ToString();
            CustomerInformation = $"{customerName} | {customerPhoneNumber}";

            SubTotal = orderGroup.order.Total;
            Total = orderGroup.order.Balance;
            SpecialNotes = orderGroup.order.DeliveryFee > 0 ? ("Deliver fee: $" + orderGroup.order.DeliveryFee) : "";
            
        }
        public InvoiceViewModel()
        {

        }


    }
}
