using GestionInventario.src.Modules.Categories.Domain.DTOs;

namespace GestionInventario.src.Modules.Categories.Services
{
    public interface ICategoryService
    {
        void AddCategory(CategoryDto categoryDto);
        bool DeleteCategory(CategoryDto categoryDto);
        IEnumerable<CategoryDto> GetAllCategories();
        CategoryDto? GetCategoryByName(string name);
        bool UpdateCategory(CategoryDto categoryDto);
    }
}