using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels
{
    public class OrderRetrievalModel
    {
        public List<Order> Orders { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Product> Products { get; set; }
        public OrderRetrievalModel(List<Order> _orders, List<Customer> _customers, List<Payment> _payments, List<Product> _products)
        {
            Orders = _orders;
            Customers = _customers;
            Payments = _payments;
            Products = _products;
        }

        public OrderRetrievalModel()
        {

        }
    }
}

    
