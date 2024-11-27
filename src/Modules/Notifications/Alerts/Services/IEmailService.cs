
namespace GestionInventario.src.Modules.Notifications.Alerts.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}