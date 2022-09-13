using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
            List<Order> viewModel = await _webApiService.GetOrdersAsync(false);
            viewModel = viewModel.OrderBy(x => x.FulfillmentDate).ToList();

            ViewData["orderSummary"] = "active";

            return View(viewModel);
        }
    }
}