using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
