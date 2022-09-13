using InventoryManagementSystem.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace InventoryManagementSystem.Services
{
    public class SendGridService : ISendGrid
    {
        public SendGridService()
        {

        }

        

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            await Execute("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", subject, message, toEmail);
        }

        private async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("j.r.woodworking@protonmail.com"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            var dummy = response.StatusCode;
            var dummy2 = response.Headers;
        }
    }
}
