using GestionInventario.Models;
using GestionInventario.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.Controller;
[ApiController]
[Route("api/[controller]")]
public class AuthController :ControllerBase{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

[HttpPost("validate")]
public IActionResult validateUser([FromBody] AuthRequest request){
    if (request == null || string.IsNullOrEmpty(request.Email)
        || string.IsNullOrEmpty(request.Password))   return BadRequest("Correo y contraseña son requeridos.");
    var authResponse = _authService.ValidateUser(request.Email,request.Password);
    if (!authResponse.IsSuccessful) return Unauthorized("Correo y contraseña inválidos.");
    return Ok(authResponse);
}
}