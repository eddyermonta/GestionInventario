using GestionInventario.src.Modules.UsersRolesManagement.Roles.Dtos;
using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.DTOs;
using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.DTOS;

namespace GestionInventario.src.Modules.UsersRolesManagement.Users.Services;

public interface IUserService
{
    Task<UserResponse?> AddUser(UserRequest userRequest, RoleRequest roleRequest);
    Task<bool> UpdateUser(UserUpdateRequest userUpdateRequest, string email);
    Task<UserResponse?> GetUserByEmail(string email);
    Task<UserResponse?> GetUserById(string id);
    Task<IEnumerable<UserResponse>>  GetAllUsers();
}