namespace GestionInventario.Models;
using System.ComponentModel.DataAnnotations;

public class User{
    public Guid Id { get; set; }

    [StringLength(100)]
    public required string Name { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }
    public DocumentType DocumentType { get; set; }

    [StringLength(20)]
    public required string DocumentNumber { get; set; }

    public required Adress Adress { get; set; }

    [StringLength(15)]
    public required string PhoneNumber { get; set; }
    public bool Isactive { get; set; }
    public required string Email { get; set; }  // Correo electrónico único para cada usuario
    public required string PasswordHash { get; set; }  // Almacenamiento seguro de la contraseña
}


