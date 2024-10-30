
using GestionInventario.src.Users.Domains.Models;

namespace GestionInventario.src.Suppliers.Domains.Models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required Address Address { get; set; }
        
    }
}