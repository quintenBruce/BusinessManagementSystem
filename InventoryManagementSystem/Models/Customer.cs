using System.ComponentModel.DataAnnotations;
namespace InventoryManagementSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string First_name { get; set; }
        [Display(Name = "Middle Name")]
        public string? Middle_name { get; set; }
        [Display(Name = "Last Name")]
        public string? Last_name { get; set; }
        [Display(Name = "Phone Number")]
        public int? Phone_number { get; set; }
    }
}
