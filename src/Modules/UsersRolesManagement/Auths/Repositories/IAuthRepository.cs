using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.Models;

namespace GestionInventario.src.Modules.UsersRolesManagement.Auths.Repositories
{
    public interface IAuthRepository
    {
       Task<bool> ValidateUserAsync(User user, string password);
    }
}