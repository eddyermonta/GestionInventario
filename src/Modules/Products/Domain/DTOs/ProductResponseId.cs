using System.ComponentModel.DataAnnotations;
using GestionInventario.src.Modules.Products.Domain.Models;
namespace GestionInventario.src.Modules.Products.Domain.DTOs
{
    public class ProductResponseId
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Initial_Amount { get; set; }
        public required decimal UnitPrice { get; set; } 
        public string? ExpirationDate { get; set; }
        public Mesurement? Weight { get; set; }  
        public List<string> Categories { get; set; } = [];
        
    }
}