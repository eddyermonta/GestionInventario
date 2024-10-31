using System.ComponentModel.DataAnnotations;
using GestionInventario.src.Modules.Users.Domains.DTOs;

namespace GestionInventario.src.Modules.Suppliers.Domains.DTOs
{
    public class SupplierDto
    {
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Phone is required.")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public required string Phone { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public required string Email { get; set; }
    public required AddressDto Address { get; set; }
    }
}