using GestionInventario.src.Modules.Suppliers.Domains.Models;

namespace GestionInventario.src.Modules.Suppliers.Repositories
{
    public interface ISupplierRepository
    {
        Task<Supplier?> GetSupplierByNIT(string NIT);
        Task<Supplier?> GetSupplierByName(string name);
        Task<IEnumerable<Supplier?>> GetAllSuppliers();
        Task CreateSupplier(Supplier supplier);
        Task UpdateSupplier(Supplier supplier);
        Task DeleteSupplierByNIT(Supplier supplier);
        
    }
}