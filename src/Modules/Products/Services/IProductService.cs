using GestionInventario.src.Modules.Products.Domain.DTOs;
namespace GestionInventario.src.Modules.Products.Services
{
    public interface IProductService
    {
        Task<ProductResponse?> GetProductByName(string name);
        Task<IEnumerable<ProductResponse>> GetAllProducts();
        Task<ProductResponseId?> AddProduct(ProductRequest productRequest, Guid supplierId);
        Task<bool> UpdateProduct(ProductResponse productResponse, string name);
        Task<bool> DeleteProduct(string name);
        
    }
}