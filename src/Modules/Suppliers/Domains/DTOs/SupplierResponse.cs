using GestionInventario.src.Modules.Users.Domains.DTOS;

namespace GestionInventario.src.Modules.Suppliers.Domains.DTOs
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