using GestionInventario.src.Modules.ProductCategories.Domain.DTOs;

namespace GestionInventario.src.Modules.ProductCategories.Services
{
    public interface IProductCategoryService
    {
        bool AddProductCategory(ProductCategoryRequest productCategoryRequest);
    }
}