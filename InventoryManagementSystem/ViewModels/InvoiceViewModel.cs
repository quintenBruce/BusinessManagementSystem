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

        public InvoiceViewModel(Order order)
        {
            ItemDescriptions = new List<string>() { "", "", "", "", "", "", "", "" };
            ItemDescriptions = order.Products!.Select(x => (x.Name != null ? (x.Name + " | ") : "") + (x.Dimensions != null ? (x.Dimensions + " | ") : "") + (x.Description != null ? (x.Description) : "")).ToList();
            ItemUnitCosts = ItemAmounts = order.Products!.Select(x => (double)x.Price).ToList();

            PaymentDescriptions = order.Payments!.Select(x => "Payment via " + x.Type.ToString()).ToList();
            PaymentUnitCosts = PaymentAmounts = order.Payments!.Select(x => (double)x.Amount).ToList();

            PaymentQTYs = ItemQTYs = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 };

            string customerName = order.Customer.FullName;
            string customerPhoneNumber = order.Customer.PhoneNumber.ToString()!;
            CustomerInformation = $"{customerName} | {customerPhoneNumber}";

            SubTotal = order.Total;
            Total = order.Balance;
            SpecialNotes = order.DeliveryFee > 0 ? ("Deliver fee: $" + order.DeliveryFee) : "";
        }

        public InvoiceViewModel()
        {
        }
    }
}