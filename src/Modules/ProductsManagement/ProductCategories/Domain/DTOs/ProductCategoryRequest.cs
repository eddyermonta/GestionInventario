
using System.ComponentModel.DataAnnotations;

namespace GestionInventario.src.Modules.ProductsManagement.ProductCategories.Domain.DTOs
{
    public class ProductCategoryRequest
    {
        [Required (ErrorMessage = "El campo ProductId es requerido.")]
        public required Guid ProductId { get; set; }
        [Required (ErrorMessage = "El campo CategoryId es requerido.")]
        public required Guid CategoryId { get; set; }
        
    }
}