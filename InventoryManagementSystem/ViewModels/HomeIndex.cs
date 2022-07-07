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
            orderRetrievalModel.allProducts = new List<Product>();
            orderRetrievalModel.allCustomers = new List<Customer>();
            orderRetrievalModel.allOrders = new List<Order>();
            orderRetrievalModel.allPaymentHistories = new List<PaymentHistory>();
        }
    }
}
