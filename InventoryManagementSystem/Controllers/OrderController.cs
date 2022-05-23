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
            var retrievedCategories = await categoryInterface.GetAllCategories();

            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.categories = retrievedCategories;



            IProduct productInterface = new ProductService();

            

            orderViewModel.orderRetrievalModel.allProducts = await productInterface.GetAllProducts();
            



            using (OrdersContext context = new OrdersContext())
            {



                orderViewModel.orderRetrievalModel.allProducts = context.Products.Include(product => product.Category).Include(product => product.Order).ToList();
                orderViewModel.orderRetrievalModel.allCustomers = context.Customers.ToList();
                orderViewModel.orderRetrievalModel.allOrders = context.Orders.ToList();
                orderViewModel.orderRetrievalModel.allPaymentHistories = context.PaymentHistories.Include(payment => payment.Order).ToList();

            }

            Order order = new Order();
            ViewData["SingleOrder"] = order;

            return View(orderViewModel);
        }







        [HttpPost]
        public ActionResult Index(OrderViewModel orderViewModel)
        {


            


            for (int i = 0; i < orderViewModel.products.Count(); i++)
            {
                orderViewModel.products[i].Order = orderViewModel.order;
                orderViewModel.order.Total += orderViewModel.products[i].Price;
            }

            



            if (orderViewModel.paymentHistory != null)
            {
                orderViewModel.paymentHistory.Order = orderViewModel.order;
                orderViewModel.order.Balance = orderViewModel.order.Total - orderViewModel.paymentHistory.PaymentAmount;
            }
                


            orderViewModel.order.Order_date = DateTime.Now; 
            IOrderGroup orderGroupService = new OrderGroupService();
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
    }
}
