

using GestionInventario.src.Modules.UsersRolesManagement.Addresses.Domains.DTOs;

namespace GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Domains.DTOs
{
    public class SupplierResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NIT { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public  AddressResponse? Address { get; set; }
        
    }
}