namespace GestionInventario.src.Modules.UsersRolesManagement.Auths.Domains.DTOs;

public class AuthResponse{
    public bool IsSuccessful { get; set; }
    public string? Jwt { get; set; }
    public required string Message { get; set; }
}