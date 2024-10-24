using GestionInventario.src.Users.Domains.Models;

namespace GestionInventario.src.Shared
{
    public interface IAuthUserCache
    {
        Dictionary<string, User> GetUsersByEmail();
    }
}