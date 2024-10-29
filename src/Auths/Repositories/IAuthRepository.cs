
using GestionInventario.src.Users.Domains.Models;

namespace GestionInventario.src.Auths.Repositories
{
    public interface IAuthRepository
    {
        User? GetUserByEmail(string email);
    }
}