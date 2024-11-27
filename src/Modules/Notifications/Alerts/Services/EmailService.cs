using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace GestionInventario.src.Modules.Notifications.Alerts.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Inventory System", "noreply@company.com"));
            message.To.Add(new MailboxAddress("test",to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.mailtrap.io", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("your_username", "your_password");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}