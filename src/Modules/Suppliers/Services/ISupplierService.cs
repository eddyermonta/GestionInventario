using GestionInventario.src.Modules.Suppliers.Domains.DTOs;
namespace GestionInventario.src.Modules.Suppliers.Services
{
    public interface ISupplierService
    {
        SupplierDto? GetSupplierByNIT(string NIT);
        IEnumerable<SupplierDto> GetAllSuppliers();
        void AddSupplier(SupplierDto supplierDto);
        bool UpdateSupplier(SupplierUpdateDto supplierDto, string NIT);
        bool DeleteSupplierByNIT(string NIT);
    }
}