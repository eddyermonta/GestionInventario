
using GestionInventario.src.Modules.UsersRolesManagement.Auths.Domains.DTOs;
using GestionInventario.src.Modules.UsersRolesManagement.Auths.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Modules.UsersRolesManagement.Auths.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase{

    private readonly IAuthService _authService = authService;

    /// <summary>
        /// Validates if the user exists in the database.
        /// </summary>
        /// <param name="authRequest">
        /// Object containing the user's email and password.
        /// </param>
        /// <returns>
        /// Returns an AuthResponse object with the authentication token.
        /// </returns>
        /// <response code="200">Returns an AuthResponse object with the authentication token.</response>
        /// <response code="401">Returns an AuthResponse object with an incorrect response.</response>
        /// <response code="400">Returns a bad request for incorrect parameters.</response>
    [HttpPost("validate")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    public async Task<IActionResult> ValidateUser([FromBody] AuthRequest authRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var authResponse = await _authService.ValidateUserAsync(authRequest.Email, authRequest.Password);

            if (!authResponse.IsSuccessful)
                return Unauthorized(authResponse.Message); // Se devuelve el mensaje de error desde el servicio

            return Ok(authResponse);
        }
}