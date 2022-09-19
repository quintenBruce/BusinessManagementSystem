using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace InventoryManagementSystem.Models
{
    public class Category
    {
        [Required(ErrorMessage = "Item category is required.")]
        public int Id { get; set; }
        
        
        public string? Name { get; set; }
    }
}