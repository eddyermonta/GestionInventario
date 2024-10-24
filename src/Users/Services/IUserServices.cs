using GestionInventario.src.Users.Domains.DTOs;

namespace GestionInventario.src.Users.Services;

public interface IUserServices
{
    void AddUser(UserDto user);
    void UpdateUser(UserDto user, string email);
    UserDto? GetUserByEmail(string email);
    IEnumerable<UserDto> GetAllUsers();
}