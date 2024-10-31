namespace GestionInventario.src.Modules.Users.Domains.DTOs;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GestionInventario.src.Modules.Users.Domains.Models.Enums;

public class UserDto {

    [StringLength(30, MinimumLength = 5, ErrorMessage = "The name must be between 5 and 30 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The name can only contain letters and spaces")]
    [Required(ErrorMessage = "The name is required")]
    public required string Name { get; set; }

    [StringLength(30, ErrorMessage = "The last name must be lower than 30 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The last name can only contain letters and spaces")]
    public string? LastName { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DocumentType DocumentType { get; set; }

    [RegularExpression(@"^\d{5,15}$", ErrorMessage = "The document number must be between 5 and 15 digits.")]
    [Required(ErrorMessage = "The document number is required")]
    public required string DocumentNumber { get; set; }

    public AddressDto? Address { get; set; }

    [StringLength(15, ErrorMessage = "The phone number must be lower than 15 characters")]
    [Required(ErrorMessage = "The phone number is required")]
    [RegularExpression(@"^\d+$", ErrorMessage = "The phone number must only contain digits")]
    public required string PhoneNumber { get; set; }
    public bool IsActive { get; set; }

    [Required(ErrorMessage = "The email is required")]
    [EmailAddress(ErrorMessage = "The email is not valid")]
    public required string Email { get; set; }

    [StringLength(100, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "The password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    public string? Password { get; set; } 

}