using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.Products.Domain.Models;

namespace GestionInventario.src.Modules.Categories.Repositories
{
    public interface ICategoryRepository
    {
        Category GetCategoryByName(string name);
        IEnumerable<Product> GetProductsByCategoryName(string categoryName);
        IEnumerable<Category> GetAllCategories();
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        
    }
}