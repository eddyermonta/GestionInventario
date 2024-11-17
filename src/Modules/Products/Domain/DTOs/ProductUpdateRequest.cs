using GestionInventario.src.Modules.Products.Domain.Models;

namespace GestionInventario.src.Modules.Products.Domain.DTOs
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