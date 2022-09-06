using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderGroup _orderGroupService;
        private readonly IProduct _productService;
        private WebApiService _webApiService;

        public OrderController(IOrderGroup orderGroupService, IProduct productService, WebApiService webApiService)
        {
            _orderGroupService = orderGroupService;
            _productService = productService;
            _webApiService = webApiService;
        }

        public async Task<ViewResult> Index()
        {
            OrderIndex viewModel = new();

            viewModel.Orders = await _webApiService.GetOrdersAsync();

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

        public async Task<PartialViewResult> UpdateOrder(Order updatedOrder, Customer updatedCustomer)
        {
            updatedOrder.Customer = updatedCustomer;
            Order newOrder = await _webApiService.UpdateOrder(OrderDTO.ToOrderDTO(updatedOrder));
            return PartialView("~/Views/Order/_OrderCustomerDetailsPartial.cshtml", newOrder);
        }

        public ActionResult SearchByName(string searchQuery)
        {
            searchQuery = searchQuery.ToLower().Trim();

            List<Order> orders = new();
            OrderRetrievalModel viewModel = new();

            using (OrdersContext context = new())
            {
                foreach (var _order in context.Orders.Include(order => order.Customer).ToList())
                {
                    var name = _order.Customer.FullName.ToLower().Replace(" ", "");

                    if (name.Contains(searchQuery))
                    {
                        viewModel.Products.AddRange(context.Products.Include(product => product.Category).Include(product => product.Order).Where(product => product.Order == _order).ToList());
                        viewModel.Orders.Add(_order);
                        viewModel.Customers.Add(_order.Customer);
                        viewModel.Payments.AddRange(context.Payments.Where(payment => payment.Order == _order).ToList());
                    }
                }
            }

            return View("~/Views/Home/Index.cshtml", viewModel);
        }
    }
}