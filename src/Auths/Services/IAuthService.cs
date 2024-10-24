using GestionInventario.src.Auths.Domains.DTOs;

namespace GestionInventario.src.Auths.Services;

public interface IAuthService
    {
        AuthResponse ValidateUser(string email, string password);
    }