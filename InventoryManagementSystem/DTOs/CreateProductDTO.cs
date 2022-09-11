using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DTOs
{
    public class CreateProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Category? Category { get; set; }
        public float Price { get; set; }
        public string? Dimensions { get; set; }
        public int OrderId { get; set; }
        public int CategoryId { get; set; }
    }
}
