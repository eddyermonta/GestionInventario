

using GestionInventario.src.Auths.Domains.DTOs;
using GestionInventario.src.Auths.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Auths.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase{

    private readonly IAuthService _authService = authService;

    [HttpPost("validate")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    public IActionResult ValidateUser([FromBody] AuthRequest authRequest)
    {
         if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var authResponse = _authService.ValidateUser(authRequest.Email, authRequest.Password);
        
        if (!authResponse.IsSuccessful) 
            return Unauthorized("Correo y contraseña inválidos.");
       
       return Ok(authResponse);
    }
}