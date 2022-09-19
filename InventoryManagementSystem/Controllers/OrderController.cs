using Anvil.Payloads.Response;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet]
        public async Task<ViewResult> CreateOrder()
        {
            ViewData["productCategories"] = await _webApiService.GetCategoriesAsync();
            var sldkff = TempData["Error Message"];
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderViewModel model)
        {
            if (model.ComThread.Trim() == "Select a communication thread") 
            {
                ModelState.AddModelError("error", "Communication thread is required");
            }
            if (!ModelState.IsValid)
            {
                
                var errors = ModelState.Select(x => x.Value!.Errors)
                           .Where(y => y.Count > 0)
                           .Select(x => x.ElementAt(0).ErrorMessage)
                           .ToList();

                ViewData["productCategories"] = await _webApiService.GetCategoriesAsync();
                return View(model);
            }

            var order = CreateOrderViewModel.ToOrder(model);
            

            if (order.Payments!.ElementAt(0).Amount == 0)
            {
                order.Payments = null;
            }

            foreach (var product in model.Products!)
            {
                product.Category = await _webApiService.GetCategoryAsync(product.Category!.Id);
            }

            order.Total += order.Products!.Sum(x => x.Price) ?? 0;
            order.Total += order.DeliveryFee ?? 0;
            order.Balance = order.Total;
            order.Balance -= order.Payments is not null ? order.Payments.Sum(x => x.Amount) : 0;

            order.PlacementDate = DateTime.Now;

            OrderDTO orderDTO = OrderDTO.ToOrderDTO(order);

            var addedOrder = await _webApiService.CreateOrderAsync(orderDTO);
            
            if (addedOrder is null)
            {
                ViewData["Error Message"] = "An error occured while processing your request. Please try again. If an error still occurs, contact the development team";
                ViewData["productCategories"] = await _webApiService.GetCategoriesAsync();
                return View(model);
            }
                

            
            ViewData["categories"] = await _webApiService.GetCategoriesAsync();
            ViewData["paymentTypeCategories"] = new List<string>() { "Venmo", "Cash App", "Cash", "Other" };
            return View(@"~/Views/Order/OrderDetails.cshtml", addedOrder);
        }

        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            var status = await _webApiService.DeleteOrder(orderId);
            
            if (!status)
                return RedirectToAction("OrderDetails", new {id=orderId});

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> CompleteOrder(int id)
        {
            var status = await _webApiService.CompleteOrder(id);
            if (!status)
                return RedirectToAction("OrderDetails", new { id = id });
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
            //Null case?
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