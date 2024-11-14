using GestionInventario.src.Data;
using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Modules.Categories.Repositories
{
    public class CategoryRepository
    (
        MyDbContext myDbContext
    )
        : ICategoryRepository
    {
        private readonly MyDbContext _context = myDbContext;

        public async Task CreateCategory(Category category)
        {
            _context.CategoriesBD.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(Category category)
        {
            _context.CategoriesBD.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await Task.FromResult(_context.CategoriesBD.ToList());
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await Task.FromResult(_context.CategoriesBD.FirstOrDefault(c => c.Name == name)!);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryName(string categoryName)
        {
            return await _context.CategoriesBD
        .Include(c => c.ProductCategories) // Incluir ProductCategories
        .ThenInclude(pc => pc.Product) // Incluir Product en ProductCategories
        .ThenInclude(p => p.ProductCategories) // Incluir ProductCategories en Product
        .ThenInclude(pc => pc.Category) // Incluir Category en ProductCategories
        .Where(c => c.Name.ToLower() == categoryName.ToLower()) // Filtrar por categoría
        .SelectMany(c => c.ProductCategories.Select(pc => pc.Product)) // Seleccionar productos
        .ToListAsync(); // Convertir a lista de forma asincrónica
        }

        public async Task UpdateCategory(Category category)
        {
            _context.CategoriesBD.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}