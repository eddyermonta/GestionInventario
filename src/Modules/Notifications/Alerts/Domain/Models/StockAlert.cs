
using GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models;

namespace GestionInventario.src.Modules.Notifications.Alerts.Domain.Models
{
    public class StockAlert
    {
        public Guid Id { get; set; }
        public DateTime AlertDate { get; set; }
        public bool IsResolved { get; set; } // Para marcar alertas como atendidas
        public DateTime ResolvedDate  { get; set; }
        public bool IsConfirmed { get; set; } // Para marcar alertas como confirmadas
        public Guid ProductId { get; set; }
        public required Product Product { get; set; } // Relación con Product
        
    }
}