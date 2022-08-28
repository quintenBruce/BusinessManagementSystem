using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface IPaymentHistory
    {
        bool CreatePayment(Payment payment);
        bool DeletePayment(int paymentId);
        bool UpdatePayment(Payment updatedPayment);
    }
}
