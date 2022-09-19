using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Item name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Item category is required")]
        public Category? Category { get; set; }
        public Order? Order { get; set; }
        [Required(ErrorMessage = "Item price field is required")]
        [Range(1, Double.MaxValue, ErrorMessage = "Item price is required and must be > 0")]
        public float? Price { get; set; }
        public string? Dimensions { get; set; }
    }
}