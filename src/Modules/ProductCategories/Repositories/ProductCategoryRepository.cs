
using GestionInventario.src.Data;
using GestionInventario.src.Modules.ProductCategories.Domain.Model;

namespace GestionInventario.src.Modules.ProductCategories.Repositories
{
    public class ProductCategoryRepository(MyDbContext myDbContext) : IProductCategoryRepository
    {
        private readonly MyDbContext _context = myDbContext;
        
        public bool CreateProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategoriesBD.Add(productCategory);
            if(_context.SaveChanges()>0) return true;
            return false;

        }

        public void Remove(ProductCategory productCategory)
        {
            _context.ProductCategoriesBD.Remove(productCategory);
            _context.SaveChanges();
        }
    }
}