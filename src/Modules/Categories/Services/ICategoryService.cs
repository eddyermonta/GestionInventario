using GestionInventario.src.Modules.Categories.Domain.DTOs;

namespace GestionInventario.src.Modules.Categories.Services
{
    public interface ICategoryService
    {
        (List<CategoryDto> AddedCategories, List<string> ExistingCategories) AddCategories(List<string> namesCategories);
        bool DeleteCategory(string name);
        IEnumerable<CategoryDto> GetAllCategories();
        CategoryDto? GetCategoryByName(string name);
        bool UpdateCategory(CategoryDto categoryDto, string name);
    }
}