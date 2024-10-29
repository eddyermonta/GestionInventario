using GestionInventario.src.Shared;
using GestionInventario.src.Users.Domains.Models;

namespace GestionInventario.src.Auths.Repositories
{
    public class AuthRepository(IAuthUserCache authUserCache) : IAuthRepository
    {
        private readonly Dictionary<string, User> usersByEmail = authUserCache.GetUsersByEmail();

        public User? GetUserByEmail(string email)
        {
            return usersByEmail.TryGetValue(email, out var user) ? user : null;
            
        }
    }
}