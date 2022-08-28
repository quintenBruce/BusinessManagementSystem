using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class PaymentController : Controller
    {
        private IPaymentHistory _paymentHistoryService;

        public PaymentController(IPaymentHistory paymentHistoryService)
        {
            _paymentHistoryService = paymentHistoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult DeletePayment(int paymentId)
        {
            List<Payment> allPayments;

            using (OrdersContext context = new OrdersContext())
            {
                int orderId = context.Payments.Include(payment => payment.Order).Where(payment => payment.Id == paymentId).First().Order.Id;

                bool status = _paymentHistoryService.DeletePayment(paymentId);
                ViewData["orderId"] = orderId;

                allPayments = context.Payments.Include(payment => payment.Order).Where(payment => payment.Order.Id == orderId).ToList();
            }

            List<string> paymentCategories = new List<string>();
            paymentCategories.Add("Venmo");
            paymentCategories.Add("Cash App");
            paymentCategories.Add("Cash");
            paymentCategories.Add("Other");

            ViewData["paymentTypeCategories"] = paymentCategories;

            return PartialView("~/Views/Order/_PaymentHistoryPartial.cshtml", allPayments);
        }

        [HttpPost]
        public PartialViewResult CreatePayments(List<Payment> payments, int orderId)
        {
            List<Payment> allPayments;
            using (OrdersContext context = new OrdersContext())
            {
                foreach (Payment payment in payments)
                {
                    payment.Order = new Order();

                    payment.Order.Id = orderId;
                    _paymentHistoryService.CreatePayment(payment);
                }

                allPayments = context.Payments.Include(payment => payment.Order).Where(payment => payment.Order.Id == orderId).ToList();
            }

            List<string> paymentCategories = new List<string>();
            paymentCategories.Add("Venmo");
            paymentCategories.Add("Cash App");
            paymentCategories.Add("Cash");
            paymentCategories.Add("Other");

            ViewData["paymentTypeCategories"] = paymentCategories;
            ViewData["orderId"] = orderId;

            return PartialView("~/Views/Order/_PaymentHistoryPartial.cshtml", allPayments);
        }

        public PartialViewResult UpdatePayment(List<Payment> payments)
        {
            using (OrdersContext context = new OrdersContext())
            {
                var orderId = payments[0].Order.Id;
                foreach (var payment in payments)
                    _paymentHistoryService.UpdatePayment(payment);

                var allPayments = context.Payments.Include(payment => payment.Order).Where(payment => payment.Order.Id == orderId).ToList();

                List<string> paymentCategories = new List<string>();
                paymentCategories.Add("Venmo");
                paymentCategories.Add("Cash App");
                paymentCategories.Add("Cash");
                paymentCategories.Add("Other");

                ViewData["paymentTypeCategories"] = paymentCategories;

                ViewData["orderId"] = orderId;

                return PartialView("~/Views/Order/_PaymentHistoryPartial.cshtml", allPayments);
            }
        }
    }
}