using GestionInventario.src.Modules.Users.Domains.DTOs;
using GestionInventario.src.Modules.Users.Domains.DTOS;

namespace GestionInventario.src.Modules.Users.Services;

public interface IUserService
{
    void AddUser(UserDto userDto);
    bool UpdateUser(UserUpdateDto userUpdateDto, string email);
    UserDto? GetUserByEmail(string email);
    IEnumerable<UserDto> GetAllUsers();
}