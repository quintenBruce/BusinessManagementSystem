using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Services
{
    public class PaymentHistoryService : IPaymentHistory
    {
        private readonly OrdersContext _ordersContext;

        public PaymentHistoryService(OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
        }

        public bool CreatePayment(Payment payment)
        {
            int orderId = payment.Order.Id;
            payment.Order = null;

            _ordersContext.Payments.Add(payment);
            payment.Order = _ordersContext.Orders.Where(order => order.Id == orderId).First();
            payment.Order.Balance = payment.Order.Balance - payment.Amount;

            var status = _ordersContext.SaveChanges();

            return status == 0 ? false : true;
        }

        public bool DeletePayment(int paymentId)
        {
            var payment = _ordersContext.Payments.Include(payment => payment.Order).Where(payment => payment.Id == paymentId).First();
            _ordersContext.Payments.Remove(payment);
            payment.Order.Balance = payment.Order.Balance + payment.Amount;

            var status = _ordersContext.SaveChanges();

            return status == 0 ? false : true;
        }

        public bool UpdatePayment(Payment updatedPayment)
        {
            var existingPayment = _ordersContext.Payments.AsNoTracking().First(payment => payment.Id == updatedPayment.Id);

            int orderId = updatedPayment.Order.Id;
            existingPayment.Order = null;
            updatedPayment.Order = null;

            existingPayment.Type = existingPayment.Type.Trim();
            updatedPayment.Type = updatedPayment.Type.Trim();

            var sldkfhjsd = JsonConvert.SerializeObject(existingPayment);
            var lkjg = JsonConvert.SerializeObject(updatedPayment);

            bool equality = JsonConvert.SerializeObject(existingPayment) == JsonConvert.SerializeObject(updatedPayment);

            if (equality)
            {
                return false;
            }
            else
            {
                updatedPayment.Order = _ordersContext.Orders.First(order => order.Id == orderId);
                _ordersContext.Update(updatedPayment);
                var ylksdf = _ordersContext.SaveChanges() == 1 ? true : false;

                return true;
            }
        }
    }
}