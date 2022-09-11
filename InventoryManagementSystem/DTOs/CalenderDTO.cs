using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DTOs
{
    public class CalenderDTO
    {
        public int Id { get; set; }
        public int NumProducts { get; set; }
        public DateTime FulfillmentDate { get; set; }

        public CalenderDTO ToCalenderDTO(Order order)
        {
            return new CalenderDTO
            {
                Id = order.Id,
                NumProducts = order.Products.Count,
                FulfillmentDate = order.FulfillmentDate,
            };
        }
    }
}