namespace GestionInventario.src.Modules.Users.Domains.Models;

using GestionInventario.src.Modules.Users.Domains.Models.Enums;
public class User{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public DocumentType DocumentType { get; set; }
    public string? DocumentNumber { get; set; }
    public  string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public  string? Email { get; set; }
    public  string? PasswordHash { get; set; } // Almacenamiento seguro de la contraseña
    // foreign key to Address
    public Guid AddressId { get; set; }
    public  Address? Address { get; set; }

}