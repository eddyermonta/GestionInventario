using GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models;

namespace GestionInventario.src.Modules.ProductsManagement.Products.Domain.DTOs
{
    public class ProductUpdateRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ExpirationDate { get; set; }
        public  Mesurement? Weight { get; set; }
        public List<string> Categories { get; set; } = [];
    }
}