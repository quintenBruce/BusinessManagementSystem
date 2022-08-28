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
        private readonly IOrder _orderService;
        private readonly IOrderGroup _orderGroupService;
        private readonly IProduct _productService;
        private WebApiService _webApiService;

        public OrderController(IOrder orderService, IOrderGroup orderGroupService, IProduct productService, WebApiService webApiService)
        {
            _orderService = orderService;
            _orderGroupService = orderGroupService;
            _productService = productService;
            _webApiService = webApiService;
        }

        public async Task<ViewResult> Index()
        {
            OrderIndex orderViewModel = new();

            orderViewModel.orderRetrievalModel.Products = _productService.GetProducts(); //assigning all products from database to model.submodel

            using (OrdersContext context = new())
            {
                orderViewModel.orderRetrievalModel.Products = context.Products.Include(product => product.Category).Include(product => product.Order).ToList();
                orderViewModel.orderRetrievalModel.Customers = context.Customers.ToList();
                orderViewModel.orderRetrievalModel.Orders = context.Orders.ToList();
                orderViewModel.orderRetrievalModel.Payments = context.Payments.Include(payment => payment.Order).ToList();

                ViewData["productCategories"] = context.Categories.ToList();
            }

            Order order = new Order();

            ViewData["orderSummary"] = "detailed";

            return View(orderViewModel);
        }

        [HttpPost]
        public ActionResult Index(OrderGroup orderGroup)
        {
            for (int i = 0; i < orderGroup.products.Count; i++) //iterate over products to set order foreign key to order
            {
                orderGroup.products[i].Order = orderGroup.order;
                orderGroup.order.Total += orderGroup.products[i].Price + orderGroup.order.DeliveryFee;
            }

            if (orderGroup.paymentHistory != null) //if model includes downpayment, set order foreign key to order and
            {                                          //update order balance
                orderGroup.paymentHistory[0].Order = orderGroup.order;
                orderGroup.order.Balance = orderGroup.order.Total - orderGroup.paymentHistory[0].Amount;
            }

            orderGroup.order.PlacementDate = DateTime.Now;
            _orderGroupService.CreateOrderGroup(orderGroup.products, orderGroup.order, orderGroup.paymentHistory[0], orderGroup.order.Customer);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteOrder(int orderId)
        {
            bool status = _orderService.DeleteOrder(orderId);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CompleteOrder(Order order)
        {
            _orderService.CompleteOrder(order.Id);
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