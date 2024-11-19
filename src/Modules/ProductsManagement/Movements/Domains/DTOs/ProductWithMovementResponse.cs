

namespace GestionInventario.src.Modules.ProductsManagement.Movements.Domains.DTOs
{
    public class ProductWithMovementsResponse
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public IEnumerable<MovementResponse>? Movements { get; set; }
        //kardex calculators
        //compra
        public int PurchaseAmount { get; set; }
        public int SalesAmount { get; set; }
        public int FinalAmount { get; set; }
        public decimal TotalPurchaseBalance { get; set; }
        public decimal AverageBalance { get; set; }
        public decimal FinalBalance { get; set; }

    }

}