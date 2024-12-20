namespace GestionInventario.Modules.src.Modules.UsersRolesManagement.Users.Controllers;

using System.ComponentModel.DataAnnotations;
using GestionInventario.src.Modules.UsersRolesManagement.Roles.Dtos;
using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.DTOs;
using GestionInventario.src.Modules.UsersRolesManagement.Users.Domains.DTOS;
using GestionInventario.src.Modules.UsersRolesManagement.Users.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
///  Controller for managing user data.
/// </summary>
/// <remarks>
/// This controller provides endpoints for retrieving, adding, updating, and deleting user data.
/// </remarks> 

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    /// <summary> Retrieves a user by their email address. </summary>
    /// <param name="email">The email address of the user to retrieve.</param>
    
    /// <returns>
    /// Returns an <see cref="IActionResult"/> containing the user data if found, 
    /// otherwise returns a 404 Not Found status if the user does not exist, 
    /// or a 400 Bad Request status if the email address is not valid.
    /// </returns>
    
    /// <response code="200">Returns the user data.</response>
    /// <response code="404">If the user is not found.</response>
    /// <response code="400">If the email address is not valid.</response>
    
    [HttpGet("{email}", Name = "GetUserByEmail")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserByEmail([EmailAddress] string email)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var user = await _userService.GetUserByEmail(email);
        if (user == null) return NotFound("User not found"); // Devuelve 404 si no se encuentra el usuario
        return Ok(user);
    }

    /// <summary> Retrieves a user by their id. </summary>
    /// <param name="id">The id of the user to retrieve.</param>
    /// <returns> 
    /// Returns an <see cref="IActionResult"/> containing the user data if found,
    /// otherwise returns a 404 Not Found status if the user does not exist.
    /// </returns>
    /// <response code="200">Returns the user data.</response>
    /// <response code="404">If the user is not found.</response>
    /// <response code="400">If the id is not valid.</response>
     
    [HttpGet("userId/{id}", Name = "GetUserById")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserById(string id)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var user = await _userService.GetUserById(id);
        if (user == null) return NotFound("User not found"); // Devuelve 404 si no se encuentra el usuario
        return Ok(user);
    }


    /// <summary> Retrieves all users. </summary>
    /// <returns> Returns an <see cref="IActionResult"/> containing a list of all users if found,
    /// otherwise returns a 204 No Content status if no users exist.
    /// </returns> 
    /// <response code="200">Returns the list of users.</response>
    /// <response code="204">No users found.</response>
    
    [HttpGet(Name = "GetAllUsers")]
    [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        if (!users.Any()) return NoContent(); // Devuelve 204 si no hay usuarios
        return Ok(users);  // Devuelve 200 y la lista de usuarios
    }

    /// <permission cref="System.Security.Claims.ClaimTypes.Role">ADMIN</permission>
    /// <summary> Adds a new user. </summary>
    /// <param name="user"> User to add to the system. </param>
    /// <param name="roleRequest">The role that you want to create ((0) ADMIN OR (1) AUXILIAR). </param>
    /// <returns>  Returns an <see cref="IActionResult"/> containing the user data if added successfully
    /// otherwise returns a 400 Bad Request status if the user data is not valid.
    /// </returns>
    /// <response code="201">Returns the user data.</response>
    /// <response code="400">If the user data is not valid.</response>

    [Authorize(Roles = "ADMIN")]
    [HttpPost(Name = "AddUser")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddUser([FromBody] UserRequest user, [FromRoute] RoleRequest roleRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userResponse = await _userService.AddUser(user, roleRequest);
        if (userResponse == null) return BadRequest("User already exist"); // Devuelve 400 si el usuario ya existe
        // Devuelve 201 y la ubicación del nuevo recurso
        return CreatedAtRoute("GetUserByEmail", new { email = user.Email }, userResponse);
    }

    /// <permission cref="System.Security.Claims.ClaimTypes.Role">ADMIN, AUXILIAR</permission>
    /// <summary> update a user by their email address. </summary>
    /// <param name="user"> User to update from the system. </param>
    /// <param name="email">The email address of the user to update.  </param>
    /// <returns> Returns an <see cref="IActionResult"/> containing the user data if deleted successfully,
    /// otherwise returns a 404 Not Found status if the user does not exist.
    /// </returns>
    /// <response code="204">User updated successfully.</response>
    /// <response code="404">If the user is not found.</response>
    /// <response code="400">If the email address is not valid.</response>
    
    [Authorize(Roles = "ADMIN , AUXILIAR")] 
    [HttpPut("{email}", Name = "UpdateUser")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest user, [EmailAddress] string email )
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!await _userService.UpdateUser(user, email)) return NotFound(); // Handle user not found case
        return NoContent(); // Return 204 if updated successfully
    }

}