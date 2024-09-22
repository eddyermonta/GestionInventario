

using GestionInventario.Domain.Dto;

namespace GestionInventario.Services;

public interface IAuthService
    {
        AuthResponse ValidateUser(string email, string password);
    }