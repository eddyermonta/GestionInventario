using GestionInventario.src.Data;
using GestionInventario.src.Modules.Categories.Domain.Models;

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

        public void UpdateCategory(Category category)
        {
            _context.CategoriesBD.Update(category);
            _context.SaveChanges();
        }
    }
}