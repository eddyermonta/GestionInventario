using GestionInventario.src.Users.Domains.DTOs;

namespace GestionInventario.src.Users.Repositories;
public interface IUserRepository
{
    void AddUser(UserDto userDto);
    void UpdateUser(UserDto user, string email);
    UserDto? GetUserByEmail(string email);
    IEnumerable<UserDto> GetAllUsers();
    
}

