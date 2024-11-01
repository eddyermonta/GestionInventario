using GestionInventario.src.Modules.Products.Domain.DTOs;
namespace GestionInventario.src.Modules.Products.Services
{
    public interface IProductService
    {
        ProductResponse? GetProductByName(string name);
        IEnumerable<ProductResponse> GetAllProducts();
        ProductRequestDto AddProduct(ProductRequest productRequest, Guid supplierId);
        bool UpdateProduct(ProductUpdateDto productUpdateDto, string name);
        bool DeleteProduct(string name);
        
    }
}