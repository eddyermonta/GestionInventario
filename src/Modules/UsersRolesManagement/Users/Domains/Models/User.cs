namespace GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.Models;

using GestionInventario.src.Modules.UsersRolesManagement.Addresses.Domains.Models;
using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.Models.Enums;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public DocumentType DocumentType { get; set; }
    public string? DocumentNumber { get; set; }
    public bool IsActive { get; set; }
    // foreign key to Address
    public Guid AddressId { get; set; }
    public  Address? Address { get; set; }
}