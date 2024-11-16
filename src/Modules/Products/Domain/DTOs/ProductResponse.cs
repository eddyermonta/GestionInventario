using GestionInventario.src.Modules.Products.Domain.Models;
using GestionInventario.src.Modules.Suppliers.Domains.DTOs;

namespace GestionInventario.src.Modules.Products.Domain.DTOs
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; } 
        public string? ExpirationDate { get; set; }
        public Mesurement? Weight { get; set; }
        public List<string> Categories { get; set; } = [];
        public SupplierResponse? Supplier { get; set; }
    }
}