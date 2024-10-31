using GestionInventario.src.Modules.Products.Domain.DTOs;
namespace GestionInventario.src.Modules.Products.Services
{
    public interface IProductService
    {
        ProductDto? GetProductByName(string name);
        IEnumerable<ProductDto> GetAllProducts();
        void AddProduct(ProductDto productDto);
        bool UpdateProduct(ProductDto productDto);
        bool DeleteProduct(ProductDto productDto);
        
    }
}