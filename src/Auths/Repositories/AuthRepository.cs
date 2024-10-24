using GestionInventario.src.Auths.Domains.DTOs;
using GestionInventario.src.Shared;
using GestionInventario.src.Users.Domains.Models;

namespace GestionInventario.src.Auths.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly Dictionary<string, User> usersByEmail;

        public AuthRepository(IAuthUserCache authUserCache)
        {
            usersByEmail = authUserCache.GetUsersByEmail();
        }

        public AuthResponse CreateResponse(bool isSuccessful, string message, Guid jwt){
            return new AuthResponse
                {
                    IsSuccessful = isSuccessful,
                    Message = message,
                    Jwt = jwt
                };
            }

        public AuthResponse ValidateUser(string email, string password)
        {
            if (!usersByEmail.TryGetValue(email, out var user))
                return CreateResponse(false, "El usuario no existe.", Guid.Empty);

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return CreateResponse(false, "Contraseña incorrecta.", Guid.Empty);

            return CreateResponse(true, "Usuario autenticado.", Guid.NewGuid());
        }
    }
}