

using GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models.Enum;
using GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models;

namespace GestionInventario.src.Modules.ProductsManagement.Movements.Domains.Models
{
    public class Movement
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public MovementType Type { get; set; }
        public MovementForm CategoryMov { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public required string Reason { get; set; }
        //reference to the product
        public Guid? ProductId { get; set; }
        public required Product Product { get; set; }
        
    }
}