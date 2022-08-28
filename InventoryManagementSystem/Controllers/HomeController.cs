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
            


            
            orderViewModel.orderRetrievalModel.Products = _productService.GetProducts(); //assign all products from database to model.submodel

            using (OrdersContext context = new OrdersContext())
            {
                orderViewModel.orderRetrievalModel.Products = context.Products.Include(product => product.Category).Include(product => product.Order).ToList();
                orderViewModel.orderRetrievalModel.Customers = context.Customers.ToList();
                orderViewModel.orderRetrievalModel.Orders = context.Orders.Where(order => order.Status == false).ToList();
                orderViewModel.orderRetrievalModel.Payments = context.Payments.Include(payment => payment.Order).ToList();
                ViewData["productCategories"] = context.Categories.ToList();
            }

            Order order = new Order();
            ViewData["SingleOrder"] = order;
            ViewData["orderSummary"] = "active";
            

            return View(orderViewModel);
            
        }

        
    }
}