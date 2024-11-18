using GestionInventario.src.Modules.ProductsManagement.Categories.Domain.DTOs;

namespace GestionInventario.src.Modules.ProductsManagement.Categories.Services
{
    public interface ICategoryService
    {
        Task<(List<CategoryResponseName> AddedCategories, List<string> ExistingCategories)> AddCategories(List<string> namesCategories);
        Task<bool> DeleteCategory(string name);
        Task<IEnumerable<CategoryResponseName>> GetAllCategories();
        Task<CategoryResponse?> GetCategoryByName(string name);
        Task<CategoryProductsResponse?> GetProductsByCategoryName(string categoryName);
        Task<bool> UpdateCategory(CategoryResponseName categoryResponseName, string name);
    }
}