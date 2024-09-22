namespace GestionInventario.Services;

using System.Collections.Generic;
using GestionInventario.Domain.Dto;
using GestionInventario.Repository;

public class UserServices(IUserRepository userRepository) : IUserServices{
    private readonly IUserRepository _userRepository = userRepository;

    public void AddUser(UserDto user)
    {
        _userRepository.AddUser(user);
    }
    public void UpdateUser(UserDto user,string email)
    {
        _userRepository.UpdateUser(user, email);
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public UserDto? GetUserByEmail(string email)
    {
        return _userRepository.GetUserByEmail(email);
    }

    public void ActivateUser(string email)
    {
        _userRepository.ActivateUser(email);
    }
    public void DeactivateUser(string email)
    {
        _userRepository.DeactivateUser(email);
    }
    //validacion de usuario correo y contraseña
}

