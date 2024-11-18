
using GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Domains.DTOs;

namespace GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Services
{
    public interface ISupplierService
    {
        Task<SupplierResponse?> GetSupplierByNIT(string NIT);
        Task<IEnumerable<SupplierDto>> GetAllSuppliers();
        Task AddSupplier(SupplierDto supplierDto);
        Task<bool> UpdateSupplier(SupplierUpdateDto supplierDto, string NIT);
        Task<bool> DeleteSupplierByNIT(string NIT);
        Task<SupplierResponse?> GetSupplierById(string id);
    }
}