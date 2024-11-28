
namespace GestionInventario.src.Modules.Notifications.Alerts.Domain.Models
{
    public class StockAlert
    {
         public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public DateTime AlertDate { get; set; }
        public bool IsResolved { get; set; } // Para marcar alertas como atendidas
        public int CurrentStock { get; set;}
        public int MinimumStock {get; set; }
        
    }
}