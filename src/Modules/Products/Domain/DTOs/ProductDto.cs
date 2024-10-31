using System.ComponentModel.DataAnnotations;
using GestionInventario.src.Modules.Products.Domain.Models;

namespace GestionInventario.src.Modules.Products.Domain.DTOs
{
    public class ProductDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Las categorías son obligatorias.")]
        public required List<string> Categories { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public required int Amount { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que cero.")]
        public required decimal UnitPrice { get; set; } 

        public DateTime? ExpirationDate { get; set; }

        public UnitMeasurement UnitMeasurement { get; set; }
    }
}