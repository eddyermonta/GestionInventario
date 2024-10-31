using GestionInventario.src.Modules.Suppliers.Domains.Models;

namespace GestionInventario.src.Modules.Suppliers.Repositories
{
    public interface ISupplierRepository
    {
        Supplier GetSupplierByNIT(string NIT);
        Supplier GetSupplierByName(string name);
        IEnumerable<Supplier> GetAllSuppliers();
        void CreateSupplier(Supplier supplier);
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplierByNIT(Supplier supplier);
        
    }
}