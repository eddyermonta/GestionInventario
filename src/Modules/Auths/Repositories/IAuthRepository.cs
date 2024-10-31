
using GestionInventario.src.Modules.Users.Domains.Models;

namespace GestionInventario.src.Modules.Auths.Repositories
{
    public interface IAuthRepository
    {
        User? GetUserByEmail(string email);
    }
}