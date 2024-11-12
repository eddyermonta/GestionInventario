using GestionInventario.src.Modules.Auths.Domains.DTOs;

namespace GestionInventario.src.Modules.Auths.Services;

public interface IAuthService
    {
        Task<AuthResponse> ValidateUserAsync(string email, string password);
    }