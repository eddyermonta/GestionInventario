using GestionInventario.src.Modules.Auths.Domains.DTOs;

namespace GestionInventario.src.Modules.Auths.Services;

public interface IAuthService
    {
        AuthResponse ValidateUser(string email, string password);
    }