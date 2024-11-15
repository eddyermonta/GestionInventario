
using GestionInventario.src.Modules.Users.Domains.Models;

namespace GestionInventario.src.Modules.Roles.Services
{
    public interface IRoleService
    {
        Task<bool> EnsureRolesExist(List<string> roles);
        Task<bool> AssingRoleToUser(User newUser, string role);
    }
}