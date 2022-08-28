using InventoryManagementSystem.Models;
using System.ComponentModel.DataAnnotations;




namespace InventoryManagementSystem.ViewModels
{
    public class HomeIndex
    {
        

        [Display(Name = "Category")]
        public OrderRetrievalModel orderRetrievalModel { get; set; }

        public HomeIndex()
        {
            orderRetrievalModel = new OrderRetrievalModel();
            orderRetrievalModel.Products = new List<Product>();
            orderRetrievalModel.Customers = new List<Customer>();
            orderRetrievalModel.Orders = new List<Order>();
            orderRetrievalModel.Payments = new List<Payment>();
        }
    }
}
