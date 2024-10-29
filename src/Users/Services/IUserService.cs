using GestionInventario.src.Users.Domains.DTOs;
using GestionInventario.src.Users.Domains.DTOS;

namespace GestionInventario.src.Users.Services;

public interface IUserService
{
    void AddUser(UserDto userDto);
    bool UpdateUser(UserUpdateDto userDto, string email);
    UserDto? GetUserByEmail(string email);
    IEnumerable<UserDto> GetAllUsers();
}