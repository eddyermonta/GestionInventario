using System.ComponentModel.DataAnnotations;

namespace GestionInventario.src.Modules.Auths.Domains.DTOs;
public class AuthRequest
{
    [Required(ErrorMessage = "The email is required")]
    [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,100}$",
        ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.")]
    public required string Password { get; set; }
}
