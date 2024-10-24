namespace GestionInventario.src.Auths.Domains.DTOs;

public class AuthResponse{
    public bool IsSuccessful { get; set; }
    public Guid? Jwt { get; set; }
    public required string Message { get; set; }
}