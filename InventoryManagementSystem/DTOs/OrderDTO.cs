using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public float Total { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderFulfillmentDate { get; set; }
        public string ComThread { get; set; } = null!;
        public bool OrderStatus { get; set; }
        public int DeliveryFee { get; set; }
        public bool? OutOfTown { get; set; }
        public float Balance { get; set; }
        public DateTime? OrderCompletionDate { get; set; }
        public CustomerDTO Customer { get; set; }
        public ICollection<ProductDTO>? Products { get; set; }
        public ICollection<PaymentDTO>? Payments { get; set; }


        public static OrderDTO ToOrderDTO(Order order)
        {
            return new OrderDTO 
            {
                Id = order.Id,
                Total = order.Total,
                OrderDate = order.PlacementDate,
                OrderFulfillmentDate = order.FulfillmentDate,
                ComThread = order.ComThread,
                OrderStatus = order.Status,
                DeliveryFee = order.DeliveryFee,
                OutOfTown = order.OutOfTown,
                Balance = order.Balance,
                OrderCompletionDate = order.CompletionDate,
                Customer = CustomerDTO.ToCustomerDTO(order.Customer),
                Products = order.Products is not null ? (ICollection<ProductDTO>)order.Products.Select(x => ProductDTO.ToProductDTO(x)) : (ICollection<ProductDTO>)new List<ProductDTO> { },
                Payments = order.Payments is not null ? (ICollection<PaymentDTO>)order.Payments.Select(x => PaymentDTO.ToPaymentDTO(x)) : (ICollection<PaymentDTO>)new List<PaymentDTO> { }


            };
        }

        

    }
}
