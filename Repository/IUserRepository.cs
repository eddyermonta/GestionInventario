using GestionInventario.Domain.Dto;
namespace GestionInventario.Repository;


public interface IUserRepository
{
    void AddUser(UserDto userDto);
    void UpdateUser(UserDto user, string email);
    UserDto? GetUserByEmail(string email);
    IEnumerable<UserDto> GetAllUsers();
    AuthResponse ValidateUser(string email, string password);
}

