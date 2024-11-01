using System.ComponentModel.DataAnnotations;
using GestionInventario.src.Modules.Products.Domain.Models;

namespace GestionInventario.src.Modules.Products.Domain.DTOs
{
    public class ProductUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; } 
        public string? ExpirationDate { get; set; }
        public Mesurement? Weight { get; set; }
        public List<string> CategoryNames { get; set; } = []; 
    }
}