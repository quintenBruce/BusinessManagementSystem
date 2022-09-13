using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private WebApiService _webApiService;

        public OrderController(WebApiService webApiService)
        {
            _webApiService = webApiService;
        }

        public async Task<ViewResult> Index()
        {
            OrderIndex viewModel = new()
            {
                Orders = await _webApiService.GetOrdersAsync()
            };

            viewModel.Orders = viewModel.Orders.OrderBy(x => x.Status).ThenBy(x => x.FulfillmentDate).ToList();

            ViewData["orderSummary"] = "detailed";
            ViewData["productCategories"] = await _webApiService.GetCategoriesAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(Order order)
        {
            if (order.Payments.ElementAt(0).Amount == 0)
            {
                order.Payments = null;
            }

            foreach (var product in order.Products)
            {
                product.Category = await _webApiService.GetCategoryAsync(product.Category.Id);
            }

            order.Total += order.Products.Sum(x => x.Price) + order.DeliveryFee;
            order.Balance = order.Total;
            order.Balance -= order.Payments is not null ? order.Payments.Sum(x => x.Amount) : 0;

            order.PlacementDate = DateTime.Now;

            OrderDTO orderDTO = OrderDTO.ToOrderDTO(order);

            var addedOrder = await _webApiService.CreateOrderAsync(orderDTO);
            ViewData["categories"] = await _webApiService.GetCategoriesAsync();
            ViewData["paymentTypeCategories"] = new List<string>() { "Venmo", "Cash App", "Cash", "Other" };
            return View(@"~/Views/Order/OrderDetails.cshtml", addedOrder);
        }

        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            var status = await _webApiService.DeleteOrder(orderId);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> CompleteOrder(int id)
        {
            var status = await _webApiService.CompleteOrder(id);
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> OrderDetails(int id)
        {
            Order order = await _webApiService.GetOrder(id);

            ViewData["paymentTypeCategories"] = new List<string>() { "Venmo", "Cash App", "Cash", "Other" };
            ViewData["categories"] = await _webApiService.GetCategoriesAsync();
            ViewData["orderId"] = order.Id;

            return View(order);
        }

        public async Task<PartialViewResult> UpdateOrder(Order updatedOrder)
        {
            Order newOrder = await _webApiService.UpdateOrder(OrderDTO.ToOrderDTO(updatedOrder));
            return PartialView("~/Views/Order/_OrderCustomerDetailsPartial.cshtml", newOrder);
        }

        public async Task<ActionResult> SearchByName(string searchQuery)
        {
            List<Order> orders = await _webApiService.SearchOrdersByName(searchQuery);

            ViewData["orderSummary"] = "detailed";

            return View("~/Views/Home/Index.cshtml", orders);
        }
    }
}