namespace InventoryManagementSystem.Interfaces
{
    public interface ISendGrid
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
