using GestionInventario.src.Modules.UsersRolesManagement.Auths.Domains.DTOs;

namespace GestionInventario.src.Modules.UsersRolesManagement.Auths.Services;

public interface IAuthService
    {
        Task<AuthResponse> ValidateUserAsync(string email, string password);
    }