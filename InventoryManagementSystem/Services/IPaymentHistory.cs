using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface IPaymentHistory
    {
        bool CreatePayment(PaymentHistory payment);
    }
}
