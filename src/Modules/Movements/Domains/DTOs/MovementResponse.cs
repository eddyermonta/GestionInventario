using GestionInventario.src.Modules.Movements.Domains.Models.Enum;
using GestionInventario.src.Modules.Products.Domain.Models;

namespace GestionInventario.src.Modules.Movements.Domains.DTOs
{
    public class MovementResponse
    {
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string? Reason { get; set; }
        public string? ProductName { get; set; }
        public MovementCategory CategoryMov { get; set; }
    }
}