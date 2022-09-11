using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }

        public float Price { get; set; }
        public string? Dimensions { get; set; }
        public virtual Category? Category { get; set; }

        public static ProductDTO ToProductDTO(Product p)
        {
            return new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Dimensions = p.Dimensions,
                Category = p.Category
            };
        }
    }
}