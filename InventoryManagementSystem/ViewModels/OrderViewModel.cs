using InventoryManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModels
{
    public class OrderViewModel
    {
        public Order order { get; set; }
        public Customer customer { get; set; }
        public PaymentHistory paymentHistory { get; set; }
        public List<Product> products { get; set; }
        public List<Category> categories { get; set; }
        [Display(Name = "Category")]
        public List<int> categoryIds { get; set; }
        public OrderRetrievalModel orderRetrievalModel { get; set; }

        public OrderViewModel()
        {
            orderRetrievalModel = new OrderRetrievalModel();
        }
        
    }
}
