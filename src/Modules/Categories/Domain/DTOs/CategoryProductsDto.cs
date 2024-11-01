using GestionInventario.src.Modules.Products.Domain.DTOs;
namespace GestionInventario.src.Modules.Categories.Domain.DTOs
{
    public class CategoryProductsDto
    {
        public required List<ProductResponse> Products { get; set; }
    }
}