
using GestionInventario.src.Modules.Users.Domains.Models;

namespace GestionInventario.src.Modules.Users.Repositories
{
    public interface IUserRepository
    {
        Task <bool> AddUser(User user, string Password);
        Task<bool> UpdateUser(User user);
        Task<User?> GetUserByEmail(string email);
        Task<IEnumerable<User>>  GetAllUsers();
        Task<User?> GetUserById(string id);
    }
}