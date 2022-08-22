using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModels;
using InventoryManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct _productService;
        public HomeController(IProduct productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            
            HomeIndex orderViewModel = new HomeIndex();
            


            
            orderViewModel.orderRetrievalModel.allProducts = _productService.GetProducts(); //assign all products from database to model.submodel

            using (OrdersContext context = new OrdersContext())
            {
                orderViewModel.orderRetrievalModel.allProducts = context.Products.Include(product => product.Category).Include(product => product.Order).ToList();
                orderViewModel.orderRetrievalModel.allCustomers = context.Customers.ToList();
                orderViewModel.orderRetrievalModel.allOrders = context.Orders.Where(order => order.Order_status == false).ToList();
                orderViewModel.orderRetrievalModel.allPaymentHistories = context.PaymentHistories.Include(payment => payment.Order).ToList();
                ViewData["productCategories"] = context.Categories.ToList();
            }

            Order order = new Order();
            ViewData["SingleOrder"] = order;
            ViewData["orderSummary"] = "active";
            

            return View(orderViewModel);
            
        }

        
    }
}