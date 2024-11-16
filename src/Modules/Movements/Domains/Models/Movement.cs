

using GestionInventario.src.Modules.Movements.Domains.Models.Enum;
using GestionInventario.src.Modules.Products.Domain.Models;

namespace GestionInventario.src.Modules.Movements.Domains.Models
{
    public class Movement
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public MovementType Type { get; set; }
        public MovementCategory CategoryMov { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public required string Reason { get; set; }
        //reference to the product
        public Guid? ProductId { get; set; }
        public required Product Product { get; set; }
        
    }
}