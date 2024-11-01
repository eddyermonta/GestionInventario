
using GestionInventario.src.Data;
using GestionInventario.src.Modules.ProductCategories.Domain.Model;

namespace GestionInventario.src.Modules.ProductCategories.Repositories
{
    public class ProductCategoryRepository(MyDbContext myDbContext) : IProductCategoryRepository
    {
        private readonly MyDbContext _context = myDbContext;
        
        public void CreateProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategoriesBD.Add(productCategory);
            _context.SaveChanges();
        }

        public void Remove(ProductCategory productCategory)
        {
            _context.ProductCategoriesBD.Remove(productCategory);
            _context.SaveChanges();
        }
    }
}