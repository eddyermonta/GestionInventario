using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.Models;

namespace GestionInventario.src.Core.Shared
{
    public interface IAuthUserCache
    {
         Dictionary<string, User> GetUsersByEmail();
    }
}