using GestionInventario.src.Data;
using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Modules.Categories.Repositories
{
    public class CategoryRepository(MyDbContext myDbContext) : ICategoryRepository
    {
        private readonly MyDbContext _context = myDbContext;
        public void CreateCategory(Category category)
        {
            _context.CategoriesBD.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            _context.CategoriesBD.Remove(category);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.CategoriesBD;
        }

        public Category GetCategoryByName(string name)
        {
            return _context.CategoriesBD.FirstOrDefault(c => c.Name == name)!;
        }

        public IEnumerable<Product> GetProductsByCategoryName(string categoryName)
        {
            return _context.CategoriesBD
        .Include(c => c.ProductCategories) // Incluir ProductCategories
        .ThenInclude(pc => pc.Product) // Incluir Product en ProductCategories
        .ThenInclude(p => p.ProductCategories) // Incluir ProductCategories en Product
        .ThenInclude(pc => pc.Category) // Incluir Category en ProductCategories
        .Where(c => c.Name.ToLower() == categoryName.ToLower()) // Filtrar por categoría
        .SelectMany(c => c.ProductCategories.Select(pc => pc.Product)) // Seleccionar productos
        .ToList(); // Convertir a lista
        }

        public void UpdateCategory(Category category)
        {
            _context.CategoriesBD.Update(category);
            _context.SaveChanges();
        }
    }
}