using GestionInventario.Domain.Models;

namespace GestionInventario.Repository;

public class AuthRepository : IAuthRepository
{
     private static readonly List<AuthRequest> authUsers =
        [
            new AuthRequest { Email = "test@example.com", Password = "password123" },
            new AuthRequest { Email = "test@example.com", Password = "password123" }
        ];

    public AuthResponse ValidateUser(string email, string password)
    {
        var user = authUsers.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user != null)
        {
            return new AuthResponse
            {
                IsSuccessful = true,
                Message = $"Bienvenido, autenticación exitosa",
                Jwt = Guid.NewGuid().ToString()  // Simulación de un token JWT
            };
        }

        return new AuthResponse
        {
            IsSuccessful = false,
            Message = "Email o contraseña incorrectos"
        };
    }
}