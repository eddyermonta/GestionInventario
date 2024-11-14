using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.Products.Domain.Models;

namespace GestionInventario.src.Modules.Categories.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategoryName(string categoryName);
        Task<IEnumerable<Category>> GetAllCategories();
        Task CreateCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(Category category);
        
    }
}