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
    public void UpdateUser(User user,string email)
    {
        _userRepository.UpdateUser(user, email);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public User? GetUserByEmail(string email)
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

