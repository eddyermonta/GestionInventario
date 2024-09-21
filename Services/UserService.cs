namespace GestionInventario.Services;

using System.Collections.Generic;
using GestionInventario.Models;
using GestionInventario.Repository;

public class UserServices : IUserServices{
    private readonly IUserRepository _userRepository;

    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void AddUser(User user)
    {
        _userRepository.AddUser(user);
    }
    public void UpdateUser(User user)
    {
        _userRepository.UpdateUser(user);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public User? GetUserByEmail(string email)
    {
        return _userRepository.GetUserByEmail(email);
    }

    public void ActivateUser(Guid id)
    {
        _userRepository.ActivateUser(id);
    }
    public void DeactivateUser(Guid id)
    {
        _userRepository.DeactivateUser(id);
    }
    //validacion de usuario correo y contraseña
}

