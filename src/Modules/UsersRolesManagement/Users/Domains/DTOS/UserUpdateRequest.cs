
using System.ComponentModel.DataAnnotations;
using GestionInventario.src.Modules.UsersRolesManagement.Addresses.Domains.DTOs;

namespace GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.DTOS
{
    public class UserUpdateRequest
    {
    [StringLength(30, ErrorMessage = "The name must be between 5 and 30 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The name can only contain letters and spaces")]
    public string? Name { get; set; }

    [StringLength(30, ErrorMessage = "The last name must be lower than 30 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The last name can only contain letters and spaces")]
    public string? LastName { get; set; }

    [RegularExpression(@"^\d{5,15}$", ErrorMessage = "The document number must be between 5 and 15 digits.")]
    public string? DocumentNumber { get; set; }
    public AddressUpdateRequest? Address { get; set; }

    [StringLength(15, ErrorMessage = "The phone number must be lower than 15 characters")]
    [RegularExpression(@"^\d+$", ErrorMessage = "The phone number must only contain digits")]
    public string? PhoneNumber { get; set; }
    public bool? IsActive { get; set; }

    [StringLength(100, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "The password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    public string? Password { get; set; } 
    }
}