using GestionInventario.src.Modules.ProductCategories.Domain.Model;

namespace GestionInventario.src.Modules.ProductCategories.Repositories
{
    public interface IProductCategoryRepository
    {
        bool CreateProductCategory(ProductCategory productCategory);
        void Remove(ProductCategory productCategory);
    }
}