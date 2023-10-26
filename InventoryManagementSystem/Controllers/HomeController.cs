using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private WebApiService _webApiService;

        public HomeController(WebApiService webApiService)
        {
            _webApiService = webApiService;
        }

        public async Task<IActionResult> Index()
        {
            List<Order> viewModel = await _webApiService.GetActiveOrdersAsync();
            if (viewModel.Count == 0)
            {
                TempData["Error Message"] = "Please create an order to view orders";
                return RedirectToAction("CreateOrder", "Order");
            }
            viewModel = viewModel.OrderBy(x => x.FulfillmentDate).ToList();
            ViewData["orderSummary"] = "active";

            return View(viewModel);
        }
    }
}