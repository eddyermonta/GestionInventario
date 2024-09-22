namespace GestionInventario.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class User{
    public Guid Id { get; set; }

    [StringLength(30, MinimumLength = 5, ErrorMessage = "The name must be between 5 and 30 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The name can only contain letters and spaces")]
    [Required(ErrorMessage = "The name is required")]
    public required string Name { get; set; }

    [StringLength(30, ErrorMessage = "The last name must be lower than 30 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The last name can only contain letters and spaces")]
    public string? LastName { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DocumentType DocumentType { get; set; }

    [Required(ErrorMessage = "The document number is required")]
    public required string DocumentNumber { get; set; }

    public required Adress Address { get; set; }

    [StringLength(15, ErrorMessage = "The phone number must be lower than 15 characters")]
    [Required(ErrorMessage = "The phone number is required")]
    [RegularExpression(@"^\d+$", ErrorMessage = "The phone number must only contain digits")]
    public required string PhoneNumber { get; set; }

    public bool IsActive { get; set; }
    public required string PasswordHash { get; set; } // Almacenamiento seguro de la contraseña
}


