namespace GestionInventario.Models;
using System.ComponentModel.DataAnnotations;

public class User{
    public Guid Id { get; set; }

    [StringLength(30, MinimumLength = 5, ErrorMessage = "The name must be between 5 and 10 characters")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The name can only contain letters")]
    [Required(ErrorMessage = "The name is required")]
    public required string Name { get; set; }

    [StringLength(30,ErrorMessage = "The name must be lower than 30 characters")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The name can only contain letters")]
    public string? LastName { get; set; }

    public DocumentType DocumentType { get; set; }

    public required string DocumentNumber { get; set; }

    public required Adress Adress { get; set; }

    [StringLength(15, ErrorMessage="The phone number must be lower than 15 characters")]
    [Required(ErrorMessage = "The phone number is required")]
    public required string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public required string PasswordHash { get; set; }  // Almacenamiento seguro de la contraseña
}


