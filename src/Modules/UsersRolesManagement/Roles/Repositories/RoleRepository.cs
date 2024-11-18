
using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.Models;
using Microsoft.AspNetCore.Identity;

namespace GestionInventario.src.Modules.UsersRolesManagement.Roles.Repositories
{
    public class RoleRepository(RoleManager<IdentityRole> roleManager, UserManager<User> userManager ) : IRoleRepository
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task<bool> AddUserToRole(User user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded) return false;
            return true;
        }

        public async Task<bool> EnsureRolesExist(List<string> roles)
        {
            foreach (var role in roles)
            {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded) return false;
            }
            }
            return true;
        }

        public List<string> GetRolesByUser(User user)
        {
            return [.. _userManager.GetRolesAsync(user).Result];
        }
    }
}