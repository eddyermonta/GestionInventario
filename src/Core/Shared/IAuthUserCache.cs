using GestionInventario.src.Modules.Users.Domains.Models;


namespace GestionInventario.src.Core.Shared
{
    public interface IAuthUserCache
    {
         Dictionary<string, User> GetUsersByEmail();
    }
}