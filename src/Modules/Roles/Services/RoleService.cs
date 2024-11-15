using GestionInventario.src.Modules.Roles.Repositories;
using GestionInventario.src.Modules.Users.Domains.Models;

namespace GestionInventario.src.Modules.Roles.Services
{
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        public async Task<bool> AssingRoleToUser(User newUser, string role)
        {
            var couldAssingRole = await _roleRepository.AddUserToRole(newUser, role);
            if (!couldAssingRole) return false;
            return true;
        }

        public async Task<bool> EnsureRolesExist(List<string> roles)
        {
            return await _roleRepository.EnsureRolesExist(roles);
        }
    }
}