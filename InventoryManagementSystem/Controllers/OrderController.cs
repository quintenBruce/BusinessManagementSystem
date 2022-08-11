using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class OrderController : Controller
    {
        public async Task<ViewResult> Index()
        {
            ICategory categoryInterface = new CategoryService();
            OrderIndex orderViewModel = new OrderIndex();

            IProduct productInterface = new ProductService();
            orderViewModel.orderRetrievalModel.allProducts = await productInterface.GetAllProducts(); //assigning all products from database to model.submodel

            using (OrdersContext context = new OrdersContext())
            {
                orderViewModel.orderRetrievalModel.allProducts = context.Products.Include(product => product.Category).Include(product => product.Order).ToList();
                orderViewModel.orderRetrievalModel.allCustomers = context.Customers.ToList();
                orderViewModel.orderRetrievalModel.allOrders = context.Orders.ToList();
                orderViewModel.orderRetrievalModel.allPaymentHistories = context.PaymentHistories.Include(payment => payment.Order).ToList();

                ViewData["productCategories"] = context.Categories.ToList();
            }

            Order order = new Order();

            ViewData["orderSummary"] = "detailed";

            return View(orderViewModel);
        }

        [HttpPost]
        public ActionResult Index(OrderGroup orderGroup)
        {
            for (int i = 0; i < orderGroup.products.Count(); i++) //iterate over products to set order foreign key to order
            {
                orderGroup.products[i].Order = orderGroup.order;
                orderGroup.order.Total += orderGroup.products[i].Price + orderGroup.order.DeliveryFee;
            }

            if (orderGroup.paymentHistory != null) //if model includes downpayment, set order foreign key to order and
            {                                          //update order balance
                orderGroup.paymentHistory[0].Order = orderGroup.order;
                orderGroup.order.Balance = orderGroup.order.Total -  orderGroup.paymentHistory[0].PaymentAmount;
            }

            orderGroup.order.Order_date = DateTime.Now;
            IOrderGroup orderGroupService = new OrderGroupService(); //call OrderService.CreateOrderGroup to create new order and other table rows
            orderGroupService.CreateOrderGroup(orderGroup.products, orderGroup.order, orderGroup.paymentHistory[0], orderGroup.order.Customer);

            return RedirectToAction("Index");
        }


        public ActionResult DeleteOrder(int orderId)
        {
            IOrder orderService = new OrderService();
            bool status = orderService.DeleteOrder(orderId);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CompleteOrder(Order order)
        {
            IOrder orderService = new OrderService();
            orderService.CompleteOrder(order.Id);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult OrderDetails(Order order)
        {
            OrderGroup orderGroup = new OrderGroup();
            ICategory categoryService = new CategoryService();
            IOrderGroup orderGroupService = new OrderGroupService();
            orderGroup = orderGroupService.GetOrderGroup(order.Id);

            List<Category> productCategories = new List<Category>();
            List<string> paymentCategories = new List<string>();

            using (OrdersContext context = new OrdersContext())
            {
                productCategories = context.Categories.ToList();
            }

            paymentCategories.Add("Venmo");
            paymentCategories.Add("Cash App");
            paymentCategories.Add("Cash");
            paymentCategories.Add("Other");




            ViewData["paymentTypeCategories"] = paymentCategories;
            ViewData["categories"] = productCategories;


            return View(orderGroup);
        }

        public PartialViewResult UpdateOrder(Order updatedOrder, Customer updatedCustomer)
        {
            using (OrdersContext context = new())
            {
                var existingOrder = context.Orders.Where(_order => _order.Id == updatedOrder.Id).First();
                var existingCustomer = context.Customers.Where(_customer => _customer.Id == updatedCustomer.Id).First();

                bool orderDate = updatedOrder.Order_date.ToString("yyyy/MM/dd") == existingOrder.Order_date.ToString("yyyy/MM/dd");
                bool orderCompletionDate = updatedOrder.Order_completion_date?.ToString("yyyy/MM/dd") == existingOrder.Order_completion_date?.ToString("yyyy/MM/dd");
                bool orderDueDate = updatedOrder.Order_fulfillment_date.ToString("yyyy/MM/dd") == existingOrder.Order_fulfillment_date.ToString("yyyy/MM/dd");

                bool orderBalance = updatedOrder.Balance == existingOrder.Balance;
                bool orderTotal = updatedOrder.Total == existingOrder.Total;
                bool orderComThread = updatedOrder.Com_thread == existingOrder.Com_thread.Trim();
                bool deliver = updatedOrder.DeliveryFee == existingOrder.DeliveryFee;
                bool status = updatedOrder.Order_status == existingOrder.Order_status;
                bool outoftown = updatedOrder.Out_Of_Town == existingOrder.Out_Of_Town;

                if (updatedOrder.Order_date.ToString("yyyy/MM/dd") != existingOrder.Order_date.ToString("yyyy/MM/dd"))
                    existingOrder.Order_date = updatedOrder.Order_date;
                if (updatedOrder.Order_completion_date?.ToString("yyyy/MM/dd") != existingOrder.Order_completion_date?.ToString("yyyy/MM/dd"))
                    existingOrder.Order_completion_date = updatedOrder.Order_completion_date;
                if (updatedOrder.Order_fulfillment_date.ToString("yyyy/MM/dd") != existingOrder.Order_fulfillment_date.ToString("yyyy/MM/dd"))
                    existingOrder.Order_fulfillment_date = updatedOrder.Order_fulfillment_date;
                if (updatedOrder.Balance != existingOrder.Balance)
                    existingOrder.Balance = updatedOrder.Balance;
                if (updatedOrder.Total != existingOrder.Total)
                    existingOrder.Total = updatedOrder.Total;
                if (updatedOrder.Com_thread != existingOrder.Com_thread.Trim())
                    existingOrder.Com_thread = updatedOrder.Com_thread;
                if (updatedOrder.DeliveryFee != existingOrder.DeliveryFee) 
                {
                    
                    existingOrder.Total = existingOrder.Total - existingOrder.DeliveryFee + updatedOrder.DeliveryFee; //update total and balance
                    existingOrder.Balance = existingOrder.Balance - existingOrder.DeliveryFee + updatedOrder.DeliveryFee;
                    existingOrder.DeliveryFee = updatedOrder.DeliveryFee;
                }
                    
                if (updatedOrder.Order_status != existingOrder.Order_status)
                    existingOrder.Order_status = updatedOrder.Order_status;
                if (updatedOrder.Out_Of_Town != existingOrder.Out_Of_Town)
                    existingOrder.Out_Of_Town = updatedOrder.Out_Of_Town;

                if (updatedCustomer.fullName != existingCustomer.fullName.Trim())
                {
                    existingCustomer.fullName = updatedCustomer.fullName.Trim();

                }

                if (updatedCustomer.Phone_number != existingCustomer.Phone_number)
                    existingCustomer.Phone_number = updatedCustomer.Phone_number;

                var status_ = context.SaveChanges();

                existingOrder.Customer = existingCustomer;

                

                    return PartialView("~/Views/Order/_OrderCustomerDetailsPartial.cshtml", existingOrder);
            }
        }

        public ActionResult SearchByName(string searchQuery)
        {
            searchQuery = searchQuery.ToLower().Trim();

            List<Order> orders = new();
            HomeIndex HomeViewModel = new HomeIndex();

            using (OrdersContext context = new())
            {
                foreach (var order_ in context.Orders.Include(order => order.Customer).ToList())
                {
                    var name = order_.Customer.fullName.ToLower().Replace(" ", "");


                    if (name.Contains(searchQuery))
                    {
                        HomeViewModel.orderRetrievalModel.allProducts.AddRange(context.Products.Include(product => product.Category).Include(product => product.Order).Where(product => product.Order == order_).ToList());
                        HomeViewModel.orderRetrievalModel.allOrders.Add(order_);
                        HomeViewModel.orderRetrievalModel.allCustomers.Add(order_.Customer);
                        HomeViewModel.orderRetrievalModel.allPaymentHistories.AddRange(context.PaymentHistories.Where(payment => payment.Order == order_).ToList());
                    }



                }
            }

            return View("~/Views/Home/Index.cshtml", HomeViewModel);

        }


    }
}