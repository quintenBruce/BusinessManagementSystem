using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Services
{
    public class PaymentHistoryService : IPaymentHistory
    {
        public bool CreatePayment(PaymentHistory payment)
        {
            int orderId = payment.Order.Id;
            payment.Order = null;
            using (OrdersContext context = new OrdersContext())
            {
                

                context.PaymentHistories.Add(payment);
                payment.Order = context.Orders.Where(order => order.Id == orderId).First();
                payment.Order.Balance = payment.Order.Balance - payment.PaymentAmount;

                var status = context.SaveChanges();

                return status == 0 ? false : true;

                
            }
        }

        public bool DeletePayment(int paymentId)
        {
            using (OrdersContext context = new OrdersContext())
            {

                var payment = context.PaymentHistories.Include(payment => payment.Order).Where(payment => payment.Id == paymentId).First();
                context.PaymentHistories.Remove(payment);
                payment.Order.Balance = payment.Order.Balance + payment.PaymentAmount;

                var status = context.SaveChanges();

                return status == 0 ? false : true;
            }
        }

        public bool UpdatePayment(PaymentHistory updatedPayment)
        {
            using(OrdersContext context = new OrdersContext())
            {
                var existingPayment = context.PaymentHistories.AsNoTracking().First(payment => payment.Id == updatedPayment.Id);

                int orderId = updatedPayment.Order.Id;
                existingPayment.Order = null;
                updatedPayment.Order = null;


                existingPayment.PaymentType = existingPayment.PaymentType.Trim();
                updatedPayment.PaymentType = updatedPayment.PaymentType.Trim();

                var sldkfhjsd = JsonConvert.SerializeObject(existingPayment);
                var lkjg = JsonConvert.SerializeObject(updatedPayment);


                bool equality = JsonConvert.SerializeObject(existingPayment) == JsonConvert.SerializeObject(updatedPayment);

                if (equality)
                {
                    return false;
                }

                else
                {
                    
                    updatedPayment.Order = context.Orders.First(order => order.Id == orderId);
                    context.Update(updatedPayment);
                    var ylksdf = context.SaveChanges() == 1 ? true : false;

                    return true;
                }

            }
        }
    }
}
