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
        private WebApiService _webApiService;
        public HomeController(IProduct productService, WebApiService webApiService)
        {
            _productService = productService;
            _webApiService = webApiService;
        }

        public async Task<IActionResult> Index()
        {
     
            List<Order> viewModel = await _webApiService.GetOrdersAsync();
           
            ViewData["orderSummary"] = "active";


            return View(viewModel);

        }

        
    }
}