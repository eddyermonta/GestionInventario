namespace GestionInventario.src.Modules.UsersRolesManagement.Addresses.Domains.DTOs
{
    public class AddressResponse
    {
        public int ZipCode { get; set; }
        public string? Street { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}