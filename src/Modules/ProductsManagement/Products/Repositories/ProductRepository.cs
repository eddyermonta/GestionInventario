

using GestionInventario.src.Data;
using GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Modules.ProductsManagement.Products.Repositories
{
    public class ProductRepository(MyDbContext myDbContext): IProductRepository
    {
        private readonly MyDbContext _context = myDbContext;

        public async Task<Product> CreateProduct(Product product)
        {
            _context.ProductsBD.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task DeleteProduct(Product product)
        {
           _context.ProductsBD.Remove(product);
           await _context.SaveChangesAsync(); 
        }

        public async Task<Product?> GetProductByName(string name)
        {
            return await _context.ProductsBD
            .Include(s => s.Supplier)
            .ThenInclude(a => a.Address)
            .Include(pc => pc.ProductCategories)
            .ThenInclude(c => c.Category)
            .FirstOrDefaultAsync(s => s.Name == name)!;
        }

        public async Task<IEnumerable<Product?>> GetAllProducts()
        {
            return await _context.ProductsBD
            .Include(s => s.Supplier)
            .ThenInclude(a => a.Address)
            .Include(pc => pc.ProductCategories)
            .ThenInclude(c => c.Category)
            .ToListAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _context.ProductsBD.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetProductById(Guid id)
        {
            return await _context.ProductsBD
            .Include(s => s.Supplier)                 // Include supplier information
            .ThenInclude(a => a.Address)             // Include supplier's address
            .Include(pc => pc.ProductCategories)     // Include the product's categories
            .ThenInclude(c => c.Category)            // Include the category details
            .FirstOrDefaultAsync(s => s.Id == id);   // Find the product by its ID
        }

        public async Task<bool> AnyProductWithSupplierId(Guid supplierId)
        {
            return await _context.ProductsBD.AnyAsync(p => p.SupplierId == supplierId);
        }
    }
}