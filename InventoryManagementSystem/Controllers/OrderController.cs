using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModels;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class OrderController : Controller
    {
        public async Task<ViewResult> Index()
        {

            ICategory categoryInterface = new CategoryService();
            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.categories = await categoryInterface.GetAllCategories(); //assign all category rows from database to model.categories


            IProduct productInterface = new ProductService();
            orderViewModel.orderRetrievalModel.allProducts = await productInterface.GetAllProducts(); //assing all products from database to model.submodel
            

            using (OrdersContext context = new OrdersContext()) 
            {
                orderViewModel.orderRetrievalModel.allProducts = context.Products.Include(product => product.Category).Include(product => product.Order).ToList();
                orderViewModel.orderRetrievalModel.allCustomers = context.Customers.ToList();
                orderViewModel.orderRetrievalModel.allOrders = context.Orders.ToList();
                orderViewModel.orderRetrievalModel.allPaymentHistories = context.PaymentHistories.Include(payment => payment.Order).ToList();
            }

            Order order = new Order();
            ViewData["SingleOrder"] = order;
            ViewData["orderSummary"] = "detailed";

            return View(orderViewModel);
        }







        [HttpPost]
        public ActionResult Index(OrderViewModel orderViewModel)
        {

            for (int i = 0; i < orderViewModel.products.Count(); i++) //iterate over products to set order foreign key to order
            {
                orderViewModel.products[i].Order = orderViewModel.order;
                orderViewModel.order.Total += orderViewModel.products[i].Price;
            }


            if (orderViewModel.paymentHistory != null) //if model includes downpayment, set order foreign key to order and 
            {                                          //update order balance
                orderViewModel.paymentHistory.Order = orderViewModel.order;
                orderViewModel.order.Balance = orderViewModel.order.Total - orderViewModel.paymentHistory.PaymentAmount;
            }


            orderViewModel.order.Order_date = DateTime.Now; 
            IOrderGroup orderGroupService = new OrderGroupService(); //call OrderService.CreateOrderGroup to create new order and other table rows
            orderGroupService.CreateOrderGroup(orderViewModel.products, orderViewModel.order, orderViewModel.paymentHistory, orderViewModel.customer, orderViewModel.categoryIds);


            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult DeleteOrder(Order order)
        {
            IOrder orderService  = new OrderService();
            bool status = orderService.DeleteOrder(order.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CompleteOrder(Order order)
        {
            IOrder orderService = new OrderService();
            orderService.CompleteOrder(order.Id);
            return RedirectToAction("Index", "Order");
        }

        public ActionResult OrderDetails(Order order)
        {
            OrderGroup orderGroup = new OrderGroup();
            IOrderGroup orderGroupService = new OrderGroupService();
            orderGroup = orderGroupService.GetOrderGroup(order.Id);

            return View(orderGroup);
        }

        public PartialViewResult OrderDetails()
        {
            return PartialView("~/Views/Order/_OrderDetailsProductsViewPartial.cshtml");
        }
    }
}
