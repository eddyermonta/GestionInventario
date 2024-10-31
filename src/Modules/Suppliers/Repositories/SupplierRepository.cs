using GestionInventario.src.Data;
using GestionInventario.src.Modules.Suppliers.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Modules.Suppliers.Repositories{
    public class SupplierRepository(MyDbContext dbContext) : ISupplierRepository
    {
        private readonly MyDbContext _dbContext = dbContext;
        public void CreateSupplier(Supplier supplier)
        {
            _dbContext.SuppliersBD.Add(supplier);
            _dbContext.SaveChanges();
        }

        public void DeleteSupplierByNIT(Supplier supplier)
        {
            _dbContext.SuppliersBD.Remove(supplier);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return [.._dbContext.SuppliersBD.Include(s => s.Address)];
        }

        public Supplier GetSupplierByNIT(string NIT)
        {
            return _dbContext.SuppliersBD
            .Include(s => s.Address)
            .FirstOrDefault(s => s.NIT == NIT)!;
        }

          public Supplier GetSupplierByName(string name)
        {
            return _dbContext.SuppliersBD
            .Include(s => s.Address)
            .FirstOrDefault(s => s.Name == name)!;
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _dbContext.SuppliersBD.Update(supplier);
            _dbContext.SaveChanges();
        }
    }
}
