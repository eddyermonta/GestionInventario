

namespace GestionInventario.src.Modules.Notifications.Alerts.Domain.Dtos
{
    public class EmailSettings
    {
        public string? SmtpServer { get; set; }
        public int Port { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}