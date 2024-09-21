namespace GestionInventario.Services;
using GestionInventario.Models;
using GestionInventario.Repository;



public class AuthService(IAuthRepository authRepository) : IAuthService
{
    private readonly IAuthRepository _authRepository = authRepository;

    public AuthResponse ValidateUser(string email, string password)
    {
        return _authRepository.ValidateUser(email, password);
    }
}