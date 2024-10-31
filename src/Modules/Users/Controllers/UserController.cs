namespace GestionInventario.Modules.src.Modules.Users.Controllers;

using System.ComponentModel.DataAnnotations;
using GestionInventario.src.Modules.Users.Domains.DTOs;
using GestionInventario.src.Modules.Users.Domains.DTOS;
using GestionInventario.src.Modules.Users.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("{email}", Name = "GetUserByEmail")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetUserByEmail([EmailAddress] string email)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        
        var user = _userService.GetUserByEmail(email);
        if (user == null) return NotFound();
        
        return Ok(user);
    }


    [HttpGet(Name = "GetAllUsers")]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();
        if (!users.Any()) return NoContent(); // Devuelve 204 si no hay usuarios
        
        return Ok(users);  // Devuelve 200 y la lista de usuarios
    }

    [HttpPost(Name = "AddUser")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddUser([FromBody] UserDto user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        _userService.AddUser(user);

        // Devuelve 201 y la ubicación del nuevo recurso
        return CreatedAtRoute("GetUserByEmail", new { email = user.Email }, user);
    }

    [HttpPut("{email}", Name = "UpdateUser")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateUser([FromBody] UserUpdateDto user, [FromRoute] string email )
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!_userService.UpdateUser(user, email)) return NotFound(); // Handle user not found case
        
        return NoContent(); // Return 204 if updated successfully
    }

}