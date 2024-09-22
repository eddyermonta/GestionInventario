using GestionInventario.Domain.Models;

namespace GestionInventario.Services;

public interface IAuthService
    {
        AuthResponse ValidateUser(string email, string password);
    }