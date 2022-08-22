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
            List<PaymentHistory> allPayments;

            using (OrdersContext context = new OrdersContext())
            {
                int orderId = context.PaymentHistories.Include(payment => payment.Order).Where(payment => payment.Id == paymentId).First().Order.Id;

                bool status = _paymentHistoryService.DeletePayment(paymentId);
                ViewData["orderId"] = orderId;

                allPayments = context.PaymentHistories.Include(payment => payment.Order).Where(payment => payment.Order.Id == orderId).ToList();
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
        public PartialViewResult CreatePayments(List<PaymentHistory> payments, int orderId)
        {
            List<PaymentHistory> allPayments;
            using (OrdersContext context = new OrdersContext())
            {
                foreach (PaymentHistory payment in payments)
                {
                    payment.Order = new Order();

                    payment.Order.Id = orderId;
                    _paymentHistoryService.CreatePayment(payment);
                }

                allPayments = context.PaymentHistories.Include(payment => payment.Order).Where(payment => payment.Order.Id == orderId).ToList();
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

        public PartialViewResult UpdatePayment(List<PaymentHistory> payments)
        {
            using (OrdersContext context = new OrdersContext())
            {
                var orderId = payments[0].Order.Id;
                foreach (var payment in payments)
                    _paymentHistoryService.UpdatePayment(payment);

                var allPayments = context.PaymentHistories.Include(payment => payment.Order).Where(payment => payment.Order.Id == orderId).ToList();

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