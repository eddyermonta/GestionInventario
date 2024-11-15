
using GestionInventario.src.Modules.Users.Domains.Models;

namespace GestionInventario.src.Modules.Roles.Repositories
{
    public interface IRoleRepository
    {
        Task<bool> EnsureRolesExist(List<string> roles);
        Task<bool> AddUserToRole(User user, string role);
    }
}