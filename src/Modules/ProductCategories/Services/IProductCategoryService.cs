using GestionInventario.src.Modules.ProductCategories.Domain.DTOs;

namespace GestionInventario.src.Modules.ProductCategories.Services
{
    public interface IProductCategoryService
    {
        Task<bool> AddProductCategory(ProductCategoryRequest productCategoryRequest);
    }
}