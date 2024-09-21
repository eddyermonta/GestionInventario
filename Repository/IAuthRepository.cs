using GestionInventario.Models;

namespace GestionInventario.Repository;

public interface IAuthRepository{
    AuthResponse ValidateUser(string email, string password);
}