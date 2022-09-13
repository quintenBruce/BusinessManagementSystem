using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace InventoryManagementSystem.ViewModels
{
    public class LoginViewModel
    {
        
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
