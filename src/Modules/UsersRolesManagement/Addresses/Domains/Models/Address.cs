using GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Domains.Models;
using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.Models;

namespace GestionInventario.src.Modules.UsersRolesManagement.Addresses.Domains.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public required int ZipCode { get; set; }
        public required string Street { get; set; }
        public required string State { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        // Foreign keys for User
        public required Supplier Supplier { get; set; }
        // Foreign keys for User
        public required User User { get; set; }
    }
}
