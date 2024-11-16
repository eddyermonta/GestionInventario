using GestionInventario.src.Modules.Users.Domains.Models;
using Microsoft.AspNetCore.Identity;

namespace GestionInventario.src.Modules.Users.Repositories
{
    public class UserRepository(UserManager<User> userManager) : IUserRepository
    {
        private readonly UserManager<User> _userManager = userManager;


        public async Task<bool> AddUser(User user, string Password)
        {
            
            var result = await _userManager.CreateAsync(user, Password);
            if (!result.Succeeded) return false;
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
           return await Task.Run<IEnumerable<User>>(() => [.. _userManager.Users]);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<User?> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

    }
}