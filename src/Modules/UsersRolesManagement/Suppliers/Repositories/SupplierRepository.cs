using GestionInventario.src.Data;
using GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Repositories{
    public class SupplierRepository(MyDbContext dbContext) : ISupplierRepository
    {
        private readonly MyDbContext _dbContext = dbContext;
        public async Task CreateSupplier(Supplier supplier)
        {
            _dbContext.SuppliersBD.Add(supplier);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSupplierByNIT(Supplier supplier)
        {
            _dbContext.SuppliersBD.Remove(supplier);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Supplier?>> GetAllSuppliers()
        {
            return await _dbContext.SuppliersBD.Include(s => s.Address).ToListAsync();
        }

        public async Task<Supplier?> GetSupplierByNIT(string NIT)
        {
            return await _dbContext.SuppliersBD
            .Include(s => s.Address)
            .FirstOrDefaultAsync(s => s.NIT == NIT)!;
        }

        public async Task<Supplier?> GetSupplierById(string id)
        {
            return await _dbContext.SuppliersBD
            .Include(s => s.Address)
            .FirstOrDefaultAsync(s => s.Id == Guid.Parse(id))!;
        }

        public async Task<Supplier?> GetSupplierByName(string name)
        {
            return await _dbContext.SuppliersBD
            .Include(s => s.Address)
            .FirstOrDefaultAsync(s => s.Name == name)!;
        }

        public async Task UpdateSupplier(Supplier supplier)
        {
            _dbContext.SuppliersBD.Update(supplier);
            await _dbContext.SaveChangesAsync();
        }
    }
}
