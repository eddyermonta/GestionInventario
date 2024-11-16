using GestionInventario.src.Modules.Users.Domains.Models;
using Microsoft.AspNetCore.Identity;

namespace GestionInventario.src.Modules.Auths.Repositories
{
    public class AuthRepository (UserManager<User> userManager) : IAuthRepository
    {
        private readonly UserManager<User> _userManager = userManager;
        public async Task<bool> ValidateUserAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}