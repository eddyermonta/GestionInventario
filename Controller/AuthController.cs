
using GestionInventario.Domain.Dto;
using GestionInventario.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.Controller;
[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase{

    private readonly IAuthService _authService = authService;

    [HttpPost("validate")]
    public IActionResult ValidateUser([FromBody] AuthRequest authRequest){
        var authResponse = _authService.ValidateUser(authRequest.Email, authRequest.Password);
        if (!authResponse.IsSuccessful) return Unauthorized("Correo y contraseña inválidos.");
        return Ok(authResponse);
    }
}