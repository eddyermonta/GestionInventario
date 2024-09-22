namespace GestionInventario.Services;
using GestionInventario.Models;

public interface IUserServices
{
    void AddUser(User user);
    void UpdateUser(User user, string email);
    User? GetUserByEmail(string email);
    IEnumerable<User> GetAllUsers();
    void ActivateUser(string email);
    void DeactivateUser(string email);
}