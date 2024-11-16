
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestionInventario.src.Modules.Auths.Domains.DTOs;
using GestionInventario.src.Modules.Auths.Repositories;
using GestionInventario.src.Modules.Roles.Services;
using GestionInventario.src.Modules.Users.Domains.Models;
using GestionInventario.src.Modules.Users.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace GestionInventario.src.Modules.Auths.Services;


public class AuthService ( 
    IUserRepository userRepository,
    IAuthRepository authRepository,
    IRoleService roleService,
    JwtToken jwtToken
) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAuthRepository _authRepository = authRepository;
    private readonly IRoleService _roleService = roleService;
    private readonly JwtToken _jwtToken = jwtToken;

    public async Task<AuthResponse> ValidateUserAsync(string email, string password)
    {
        var isPasswordValid = false;
        string mensaje ="constraseña correcta";
        var jwt = string.Empty;

        var user = await _userRepository.GetUserByEmail(email);
        if(user == null)  mensaje = "Usuario no encontrado.";
        else{
            isPasswordValid = await _authRepository.ValidateUserAsync(user, password);
            if(!isPasswordValid) mensaje = "Contraseña incorrecta.";
            jwt =GenerateJwtToken(user);
        }
        
        return CreateResponse(isPasswordValid, mensaje, jwt);
    }

    private AuthResponse CreateResponse(bool isPasswordValid, string mensaje, string jwt){
        return new AuthResponse{
            IsSuccessful = isPasswordValid,
            Message = mensaje,
            Jwt = jwt
        };
    }

    private string GenerateJwtToken(User user)
    {
         var userRoles = _roleService.GetRolesByUser(user);
         return _jwtToken.GenerateJwtToken(user, userRoles);
    }
}