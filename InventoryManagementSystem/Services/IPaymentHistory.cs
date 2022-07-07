using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface IPaymentHistory
    {
        bool CreatePayment(PaymentHistory payment);
        bool DeletePayment(int paymentId);
        bool UpdatePayment(PaymentHistory updatedPayment);
    }
}
