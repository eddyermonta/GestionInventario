namespace GestionInventario.src.Auths.Services;

using GestionInventario.src.Auths.Domains.DTOs;
using GestionInventario.src.Auths.Repositories;

public class AuthService(IAuthRepository _authRepository) : IAuthService
{
    public AuthResponse ValidateUser(string email, string password)
    {
        return _authRepository?.ValidateUser(email, password);
    }
}