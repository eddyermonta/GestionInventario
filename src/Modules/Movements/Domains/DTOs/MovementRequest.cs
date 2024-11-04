using GestionInventario.src.Modules.Movements.Domains.Models.Enum;

namespace GestionInventario.src.Modules.Movements.Domains.DTOs
{
    public class MovementRequest
    {
        public required string ProductName { get; set; }
        public required int Amount { get; set; }
        public required MovementCategory CategoryMov { get; set; }
        public required string Reason { get; set; }
    }
}

       
        