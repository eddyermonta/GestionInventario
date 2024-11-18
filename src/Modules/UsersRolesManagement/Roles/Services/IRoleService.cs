
using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.Models;

namespace GestionInventario.src.Modules.UsersRolesManagement.Roles.Services
{
    public interface IRoleService
    {
        Task<bool> EnsureRolesExist(List<string> roles);
        Task<bool> AssingRoleToUser(User newUser, string role);
        List<string> GetRolesByUser(User user);
    }
}