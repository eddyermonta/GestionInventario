using GestionInventario.src.Modules.Suppliers.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Modules.Suppliers.Repositories{
    public class SupplierRepository(DbContext dbContext) : ISupplierRepository
    {
        private readonly DbContext _dbContext = dbContext;
        public void CreateSupplier(Supplier supplier)
        {
            _dbContext.Add(supplier);
            _dbContext.SaveChanges();
        }

        public void DeleteSupplier(Supplier supplier)
        {
            _dbContext.Remove(supplier);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return [.. _dbContext.Set<Supplier>()];
        }

        public Supplier GetSupplierByName(string name)
        {
            return _dbContext.Set<Supplier>().FirstOrDefault(s => s.Name == name)!;
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _dbContext.Update(supplier);
            _dbContext.SaveChanges();
        }
    }
}
