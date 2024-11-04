using System.ComponentModel.DataAnnotations;
using GestionInventario.src.Modules.Users.Domains.DTOs;

namespace GestionInventario.src.Modules.Suppliers.Domains.DTOs
{
    public class SupplierDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public required string Name { get; set; }
        
        [Required(ErrorMessage = "El NIT es obligatorio.")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "El NIT debe tener entre 5 y 15 caracteres.")]
        [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessage = "El NIT solo puede contener letras, números y guiones.")]
        public required string NIT { get; set; }


        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public required string Email { get; set; }
        public required AddressRequest Address { get; set; }
    }
}