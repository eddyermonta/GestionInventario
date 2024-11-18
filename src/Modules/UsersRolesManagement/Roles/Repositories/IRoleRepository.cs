
using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.Models;

namespace GestionInventario.src.Modules.UsersRolesManagement.Roles.Repositories
{
    public interface IRoleRepository
    {
        Task<bool> EnsureRolesExist(List<string> roles);
        Task<bool> AddUserToRole(User user, string role);
        List<string> GetRolesByUser(User user);
    }
}