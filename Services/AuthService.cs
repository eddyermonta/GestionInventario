namespace GestionInventario.Services;

using GestionInventario.Domain.Dto;
using GestionInventario.Repository;



public class AuthService(IUserRepository userRepository) : IAuthService

{
    private readonly IUserRepository _userRepository = userRepository;

    public AuthResponse ValidateUser(string email, string password)
    {
        return _userRepository.ValidateUser(email, password);
    }
}