using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionInventario.src.Modules.Notifications.Alerts.Domain.Dtos
{
    public class StockAlertResponse
    {
        public DateTime AlertDate { get; set; }
        public bool IsResolved { get; set; } // Para marcar alertas como atendidas
        public DateTime? ResolvedDate  { get; set; }
        public bool IsConfirmed { get; set; } // Para marcar alertas como confirmadas
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }
        public Guid ProductId { get; set; }
        
    }
}