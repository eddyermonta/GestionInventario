

using GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Domains.Models;

namespace GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Repositories
{
    public interface ISupplierRepository
    {
        Task<Supplier?> GetSupplierByNIT(string NIT);
        Task<Supplier?> GetSupplierByName(string name);
        Task<Supplier?> GetSupplierById(string id);
        Task<IEnumerable<Supplier?>> GetAllSuppliers();
        Task CreateSupplier(Supplier supplier);
        Task UpdateSupplier(Supplier supplier);
        Task DeleteSupplierByNIT(Supplier supplier);
        
    }
}