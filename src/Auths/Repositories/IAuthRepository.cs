
using GestionInventario.src.Auths.Domains.DTOs;

namespace GestionInventario.src.Auths.Repositories
{
    public interface IAuthRepository
    {
        AuthResponse ValidateUser(string email, string password);
    }
}