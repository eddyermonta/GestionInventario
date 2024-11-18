using GestionInventario.src.Modules.Products.Domain.Models;
namespace GestionInventario.src.Modules.Products.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByName(string name);
         Task<Product?> GetProductById(Guid id);
        Task<IEnumerable<Product?>> GetAllProducts();
        Task<Product> CreateProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
    }
}