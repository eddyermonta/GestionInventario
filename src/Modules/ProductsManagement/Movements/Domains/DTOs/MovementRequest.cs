

using System.ComponentModel.DataAnnotations;

namespace GestionInventario.src.Modules.ProductsManagement.Movements.Domains.DTOs
{
    public class MovementRequest
    {
        public required string ProductName { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a cero.")]
        public required int Amount { get; set; }
        public required string Reason { get; set; }
        public required decimal UnitPrice { get; set; }
    }
}

       
        