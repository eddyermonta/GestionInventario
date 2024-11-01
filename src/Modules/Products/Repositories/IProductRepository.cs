using GestionInventario.src.Modules.Products.Domain.Models;
namespace GestionInventario.src.Modules.Products.Repositories
{
    public interface IProductRepository
    {
        Product GetProductByName(string name);
        IEnumerable<Product> GetAllProducts();
        Product CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}