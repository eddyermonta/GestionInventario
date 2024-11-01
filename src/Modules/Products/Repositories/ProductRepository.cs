

using GestionInventario.src.Data;
using GestionInventario.src.Modules.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Modules.Products.Repositories
{
    public class ProductRepository(MyDbContext myDbContext) : IProductRepository
    {
        private readonly MyDbContext _context = myDbContext;
        public Product CreateProduct(Product product)
        {
            _context.ProductsBD.Add(product);
            _context.SaveChanges();
            return product;
        }
        public void DeleteProduct(Product product)
        {
            _context.ProductsBD.Remove(product);
            _context.SaveChanges();
        }

        public Product GetProductByName(string name)
        {
            return _context.ProductsBD
            .Include(s => s.Supplier)
            .Include(pc => pc.ProductCategories)
            .ThenInclude(c => c.Category)
            .FirstOrDefault(s => s.Name == name)!;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return [.. _context.ProductsBD
            .Include(s => s.Supplier)
            .Include(pc => pc.ProductCategories)
            .ThenInclude(c => c.Category)];
        }

        public void UpdateProduct(Product product)
        {
            _context.ProductsBD.Update(product);
            _context.SaveChanges();
        }


    }
}