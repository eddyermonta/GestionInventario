namespace GestionInventario.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GestionInventario.Domain.Enums;

public class User{
    public Guid Id { get; set; }

    public required string Name { get; set; }
    public string? LastName { get; set; }
    public DocumentType DocumentType { get; set; }
    public required string DocumentNumber { get; set; }

    public required Adress Address { get; set; }
    public required string PhoneNumber { get; set; }

    public bool IsActive { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; } // Almacenamiento seguro de la contraseña
}


