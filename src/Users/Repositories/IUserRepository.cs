using GestionInventario.src.Users.Domains.Models;

namespace GestionInventario.src.Users.Repositories;
public interface IUserRepository
{
    void AddUser(User user);
    void UpdateUser(User user);
    User? GetUserByEmail(string email);
    IEnumerable<User> GetAllUsers();
    
}

