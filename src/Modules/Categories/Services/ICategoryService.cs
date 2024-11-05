using GestionInventario.src.Modules.Categories.Domain.DTOs;

namespace GestionInventario.src.Modules.Categories.Services
{
    public interface ICategoryService
    {
        (List<CategoryResponseName> AddedCategories, List<string> ExistingCategories) AddCategories(List<string> namesCategories);
        bool DeleteCategory(string name);
        IEnumerable<CategoryResponseName> GetAllCategories();
        CategoryResponse? GetCategoryByName(string name);
        CategoryProductsResponse? GetProductsByCategoryName(string categoryName);
        bool UpdateCategory(CategoryResponseName categoryResponseName, string name);
    }
}