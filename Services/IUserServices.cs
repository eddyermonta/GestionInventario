namespace GestionInventario.Services;
using GestionInventario.Models;

public interface IUserServices
{
    void AddUser(User user, string email);
    void UpdateUser(User user, string email);
    User? GetUserByEmail(string email);
    IEnumerable<User> GetAllUsers();
    void ActivateUser(string email);
    void DeactivateUser(string email);
}