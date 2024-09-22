namespace GestionInventario.Domain.Dto;

public class AuthResponse{
    public bool IsSuccessful { get; set; }
    public Guid? Jwt { get; set; }
    public required string Message { get; set; }
}