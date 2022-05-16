using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class PaymentHistoryService : IPaymentHistory
    {
        public bool CreatePayment(PaymentHistory payment)
        {
            using (OrdersContext context = new OrdersContext())
            {
                

                context.PaymentHistories.Add(payment);
                var status = 0;
                return status == 0 ? false : true;
                
            }
        }
    }
}
