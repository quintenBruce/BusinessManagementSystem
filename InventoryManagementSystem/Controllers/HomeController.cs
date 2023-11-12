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
            if (!(viewModel is null || viewModel.Count == 0))
            {
                viewModel = viewModel.OrderBy(x => x.Status).ThenBy(x => x.FulfillmentDate).ToList();

                ViewData["orderSummary"] = "active";

                return View(viewModel);
            }
            

            return View(new List<Order>());
        }

        public async Task<IActionResult> GetAllOrders()
        {
            List<Order> viewModel = await _webApiService.GetOrdersAsync();
            if (!(viewModel is null || viewModel.Count == 0))
            {
                viewModel = viewModel.OrderBy(x => x.Status).ThenBy(x => x.FulfillmentDate).ToList();

                ViewData["orderSummary"] = "active";

                return View("~/Views/Home/Index.cshtml", viewModel);
            }

            return View("~/Views/Home/Index.cshtml", viewModel);
        }
    }
}