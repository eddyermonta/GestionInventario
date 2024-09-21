namespace GestionInventario.Services;

using GestionInventario.Models;

public interface IAuthService
    {
        AuthResponse ValidateUser(string email, string password);
    }