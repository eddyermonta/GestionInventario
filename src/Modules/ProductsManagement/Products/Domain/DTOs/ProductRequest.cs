using System.ComponentModel.DataAnnotations;
using GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models;
namespace GestionInventario.src.Modules.ProductsManagement.Products.Domain.DTOs
{
    public class ProductRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a cero.")]
        public required int Initial_Amount { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que cero.")]
        public required decimal UnitPrice { get; set; } 
        public string? ExpirationDate { get; set; }
        
        [Required(ErrorMessage = "La unidad de medida es obligatoria.")]
        public required Mesurement Weight { get; set; }

        //lista de categorias
        public List<string> Categories { get; set; } = [];
    }
}