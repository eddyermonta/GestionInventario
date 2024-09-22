using GestionInventario.Domain.Dto;

namespace GestionInventario.Services;

public interface IUserServices
{
    void AddUser(UserDto user);
    void UpdateUser(UserDto user, string email);
    UserDto? GetUserByEmail(string email);
    IEnumerable<UserDto> GetAllUsers();
    void ActivateUser(string email);
    void DeactivateUser(string email);
}