namespace GestionInventario.Repository;
using GestionInventario.Models;

public interface IUserRepository
{
    void AddUser(User user);
    void UpdateUser(User user);
    User? GetUserByEmail(string email);
    IEnumerable<User> GetAllUsers();
    void ActivateUser(Guid id);
    void DeactivateUser(Guid id);
}

