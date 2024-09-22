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

}

