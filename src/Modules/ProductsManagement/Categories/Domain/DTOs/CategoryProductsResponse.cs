using GestionInventario.src.Modules.ProductsManagement.Products.Domain.DTOs;
namespace GestionInventario.src.Modules.ProductsManagement.Categories.Domain.DTOs
{
    public class CategoryProductsResponse
    {
        public required List<ProductResponse> Products { get; set; }
    }
}