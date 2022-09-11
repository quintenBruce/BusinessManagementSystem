using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [Column(TypeName = "varchar(MAX)")]
        public string FullName { get; set; }

        [Display(Name = "Phone Number")]
        public int? PhoneNumber { get; set; }
    }
}