using GestionInventario.src.Modules.ProductsManagement.Products.Domain.DTOs;
namespace GestionInventario.src.Modules.ProductsManagement.Products.Services
{
    public interface IProductService
    {
        Task<ProductResponse?> GetProductByName(string name);
        Task<ProductResponse?> GetProductById(Guid productId);
        Task<IEnumerable<ProductResponse>> GetAllProducts();
        Task<ProductResponseId?> AddProduct(ProductRequest productRequest, Guid supplierId);
        Task<bool> UpdateProduct(ProductUpdateRequest productUpdateRequest, string name);
        Task<bool> DeleteProduct(string name);
        
    }
}