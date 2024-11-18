using GestionInventario.src.Modules.ProductsManagement.ProductCategories.Domain.Model;

namespace GestionInventario.src.Modules.ProductsManagement.ProductCategories.Repositories
{
    public interface IProductCategoryRepository
    {
        Task<bool> CreateProductCategory(ProductCategory productCategory);
        Task Remove(ProductCategory productCategory);
        Task<ProductCategory?> GetProductCategoryByCategoryId(Guid categoryId);
    }
}