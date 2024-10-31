using GestionInventario.src.Core.Shared;
using GestionInventario.src.Modules.Users.Domains.Models;

namespace GestionInventario.src.Modules.Auths.Repositories
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