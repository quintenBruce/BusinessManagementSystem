using InventoryManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModels
{
    public class OrderIndex
    {
        public Order Order { get; set; }

        [Display(Name = "Category")]
        public List<Order> Orders { get; set; }

        public OrderIndex()
        {
            Orders = new List<Order>();
        }

    }
}
