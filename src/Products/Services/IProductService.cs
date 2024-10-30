using GestionInventario.src.Products.Domain.DTOs;

namespace GestionInventario.src.Products.Services
{
    public interface IProductService
    {
        void AddProduct(ProductDto productDto);
        bool UpdateProduct(ProductDto productDto);
        ProductDto? GetProductById(int id);
        IEnumerable<ProductDto> GetAllProducts();
        
    }
}