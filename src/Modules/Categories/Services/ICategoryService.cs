using GestionInventario.src.Modules.Categories.Domain.DTOs;

namespace GestionInventario.src.Modules.Categories.Services
{
    public interface ICategoryService
    {
        (List<CategoryDto> AddedCategories, List<string> ExistingCategories) AddCategories(List<string> namesCategories);
        bool DeleteCategory(string name);
        IEnumerable<CategoryDto> GetAllCategories();
        CategoryResponse? GetCategoryByName(string name);
        CategoryProductsDto GetProductByName(string categoryName);
        bool UpdateCategory(CategoryDto categoryDto, string name);
    }
}