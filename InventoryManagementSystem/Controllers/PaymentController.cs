using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private WebApiService _webApiService;

        public PaymentController(WebApiService webApiService)
        {
            _webApiService = webApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> DeletePayment(int paymentId, int orderId)
        {
            bool status = await _webApiService.DeletePayment(paymentId);

            var allPayments = await _webApiService.GetPaymentsAsync(orderId);

            ViewData["paymentTypeCategories"] = new List<string>() { "Venmo", "Cash App", "Cash", "Other" };
            ViewData["orderId"] = orderId;
            return PartialView("~/Views/Order/_PaymentHistoryPartial.cshtml", allPayments);
        }

        [HttpPost]
        public async Task<PartialViewResult> CreatePayments(List<CreatePaymentDTO> payments, int orderId)
        {
            payments.ForEach(x => x.OrderId = orderId);
            var paymentCreateStatus = await _webApiService.CreatePaymentsAsync(payments);

            var allPayments = (List<Payment>)await _webApiService.GetPaymentsAsync(orderId);

            ViewData["paymentTypeCategories"] = new List<string>() { "Venmo", "Cash App", "Cash", "Other" };
            ViewData["orderId"] = orderId;

            return PartialView("~/Views/Order/_PaymentHistoryPartial.cshtml", allPayments);
        }

        public async Task<PartialViewResult> UpdatePayment(List<PaymentDTO> payments, int orderId)
        {
            var updatedPayments = await _webApiService.UpdatePayments(payments, orderId);

            ViewData["paymentTypeCategories"] = new List<string>() { "Venmo", "Cash App", "Cash", "Other" };

            ViewData["orderId"] = orderId;

            return PartialView("~/Views/Order/_PaymentHistoryPartial.cshtml", updatedPayments);
        }
    }
}