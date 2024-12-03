using GestionInventario.src.Modules.Notifications.Alerts.Domain.Dtos;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GestionInventario.src.Modules.Notifications.Alerts.Services
{
    public class EmailService(IOptions<EmailSettings> emailSettings) : IEmailService
    {
        private readonly EmailSettings _emailSettings = emailSettings.Value;
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Sistema de Inventario", to));
            message.To.Add(new MailboxAddress("Destinatario",to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };
            try{
                using var client = new SmtpClient();
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch(Exception ex){
                 // Manejar errores y lanzar excepciones
                throw new InvalidOperationException("Error al enviar el correo electrónico.", ex);
            }
        }
    }
}