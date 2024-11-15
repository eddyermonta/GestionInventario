using GestionInventario.src.Modules.Users.Domains.DTOs;
using GestionInventario.src.Modules.Users.Domains.DTOS;

namespace GestionInventario.src.Modules.Users.Services;

public interface IUserService
{
    Task<UserResponse?> AddUser(UserRequest userRequest);
    Task<bool> UpdateUser(UserUpdateRequest userUpdateRequest, string email);
    Task<UserResponse?> GetUserByEmail(string email);
    Task<UserResponse?> GetUserById(string id);
    Task<IEnumerable<UserResponse>>  GetAllUsers();
}