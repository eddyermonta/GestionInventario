

namespace GestionInventario.src.Modules.Movements.Domains.DTOs
{
    public class ProductWithMovementsResponse
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public IEnumerable<MovementResponse>? Movements { get; set; }
        public int TotalAmounts { get; set; }
        public decimal TotalPrice { get; set; }
    }

}