using GestionInventario.src.Modules.ProductCategories.Domain.Model;

namespace GestionInventario.src.Modules.ProductCategories.Repositories
{
    public interface IProductCategoryRepository
    {
        Task<bool> CreateProductCategory(ProductCategory productCategory);
        Task Remove(ProductCategory productCategory);
    }
}