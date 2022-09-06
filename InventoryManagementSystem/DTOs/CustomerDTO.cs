using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public int? PhoneNumber { get; set; }
        public static CustomerDTO ToCustomerDTO(Customer customer)
        {
            return new CustomerDTO 
            { 
                Id = customer.Id,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber ?? 0
            };
        }
    }
}
