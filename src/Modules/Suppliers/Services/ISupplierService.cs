using GestionInventario.src.Modules.Suppliers.Domains.DTOs;
namespace GestionInventario.src.Modules.Suppliers.Services
{
    public interface ISupplierService
    {
        SupplierDto? GetSupplierByName(string name);
        IEnumerable<SupplierDto> GetAllSuppliers();
        void AddSupplier(SupplierDto supplierDto);
        bool UpdateSupplier(SupplierDto supplierDto);
        bool DeleteSupplier(SupplierDto supplierDto);
    }
}