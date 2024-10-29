namespace GestionInventario.src.Auths.Services;

using GestionInventario.src.Auths.Domains.DTOs;
using GestionInventario.src.Auths.Repositories;

public class AuthService(IAuthRepository authRepository) : IAuthService
{
    private readonly IAuthRepository _authRepository = authRepository;
    public AuthResponse ValidateUser(string email, string password)
    {
        var user = _authRepository.GetUserByEmail(email);
        
        //existe el usuario 
        if (user == null) return CreateResponse(false, "El usuario no existe.", Guid.Empty);
        //verificar contraseña
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return CreateResponse(false, "Contraseña incorrecta.", Guid.Empty);
        
        return CreateResponse(true, "Usuario autenticado.", Guid.NewGuid());
    }
    private static AuthResponse CreateResponse(bool isSuccessful, string message, Guid jwt)
        {
            return new AuthResponse
            {
                IsSuccessful = isSuccessful,
                Message = message,
                Jwt = jwt
            };
        }
}