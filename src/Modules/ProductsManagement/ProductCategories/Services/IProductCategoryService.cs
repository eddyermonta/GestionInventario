using GestionInventario.src.Modules.ProductsManagement.ProductCategories.Domain.DTOs;

namespace GestionInventario.src.Modules.ProductsManagement.ProductCategories.Services
{
    public interface IProductCategoryService
    {
        Task<bool> AddProductCategory(ProductCategoryRequest productCategoryRequest);
    }
}