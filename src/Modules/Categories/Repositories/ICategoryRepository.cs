using GestionInventario.src.Modules.Categories.Domain.Models;

namespace GestionInventario.src.Modules.Categories.Repositories
{
    public interface ICategoryRepository
    {
        Category GetCategoryByName(string name);
        IEnumerable<Category> GetAllCategories();
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        
    }
}