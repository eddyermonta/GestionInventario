using System.ComponentModel.DataAnnotations;
using GestionInventario.src.Modules.Users.Domains.DTOS;

namespace GestionInventario.src.Modules.Suppliers.Domains.DTOs
{
    public class SupplierUpdateDto
    {
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }
        public AddressUpdateRequest? Address { get; set; }    
    }
}