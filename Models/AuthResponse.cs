namespace GestionInventario.Models;

public class AuthResponse{
    public bool IsSuccessful { get; set; }
    public string? Jwt { get; set; }
    public required string Message { get; set; }
}