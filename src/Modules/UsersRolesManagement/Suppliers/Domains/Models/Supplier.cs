
using GestionInventario.src.Modules.ProductsManagement.Products.Domain.Models;
using GestionInventario.src.Modules.UsersRolesManagement.Addresses.Domains.Models;
namespace GestionInventario.src.Modules.UsersRolesManagement.Suppliers.Domains.Models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public required string NIT { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required ICollection<Product> Products { get; set; } = [];    
        //foreign key for address
        public Guid AddressId { get; set; }
        public required Address Address { get; set; }
        
    }
}