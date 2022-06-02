using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModels;
using InventoryManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            ICategory categoryInterface = new CategoryService();
            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.categories = await categoryInterface.GetAllCategories(); //assign all category rows from database to model.categories


            IProduct productInterface = new ProductService();
            orderViewModel.orderRetrievalModel.allProducts = await productInterface.GetAllProducts(); //assign all products from database to model.submodel

            using (OrdersContext context = new OrdersContext())
            {
                orderViewModel.orderRetrievalModel.allProducts = context.Products.Include(product => product.Category).Include(product => product.Order).ToList();
                orderViewModel.orderRetrievalModel.allCustomers = context.Customers.ToList();
                orderViewModel.orderRetrievalModel.allOrders = context.Orders.Where(order => order.Order_status == false).ToList();
                orderViewModel.orderRetrievalModel.allPaymentHistories = context.PaymentHistories.Include(payment => payment.Order).ToList();
            }

            Order order = new Order();
            ViewData["SingleOrder"] = order;
            ViewData["orderSummary"] = "active";

            return View(orderViewModel);
            
        }
    }
}