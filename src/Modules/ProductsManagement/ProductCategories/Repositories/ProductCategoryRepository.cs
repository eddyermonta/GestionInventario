
using GestionInventario.src.Data;
using GestionInventario.src.Modules.ProductsManagement.ProductCategories.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Modules.ProductsManagement.ProductCategories.Repositories
{
    public class ProductCategoryRepository( MyDbContext myDbContext): IProductCategoryRepository
    {
        private readonly MyDbContext _context = myDbContext;
        
        public async Task<bool> CreateProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategoriesBD.Add(productCategory);
            if(await _context.SaveChangesAsync() > 0) return true;
            return false;

        }

        public async Task<ProductCategory?> GetProductCategoryByCategoryId(Guid categoryId)
        {
            return await _context.ProductCategoriesBD.FirstOrDefaultAsync(pc => pc.CategoryId == categoryId);
        }

        public async Task Remove(ProductCategory productCategory)
        {
            _context.ProductCategoriesBD.Remove(productCategory);
            await _context.SaveChangesAsync();
        }
    }
}