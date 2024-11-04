using GestionInventario.src.Modules.Users.Domains.DTOs;
using GestionInventario.src.Modules.Users.Domains.DTOS;

namespace GestionInventario.src.Modules.Users.Services;

public interface IUserService
{
    UserResponse? AddUser(UserRequest userRequest);
    bool UpdateUser(UserUpdateRequest userUpdateRequest, string email);
    UserResponse? GetUserByEmail(string email);
    IEnumerable<UserResponse> GetAllUsers();
}