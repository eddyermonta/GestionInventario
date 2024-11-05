using GestionInventario.src.Modules.Products.Domain.DTOs;
namespace GestionInventario.src.Modules.Products.Services
{
    public interface IProductService
    {
        ProductResponse? GetProductByName(string name);
        IEnumerable<ProductResponse> GetAllProducts();
        ProductResponseId? AddProduct(ProductRequest productRequest, Guid supplierId);
        bool UpdateProduct(ProductResponse productResponse, string name);
        bool DeleteProduct(string name);
        
    }
}