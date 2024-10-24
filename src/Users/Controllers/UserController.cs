namespace GestionInventario.src.Users.Controllers;

using System.ComponentModel.DataAnnotations;
using GestionInventario.src.Users.Domains.DTOs;
using GestionInventario.src.Users.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserServices userService) : ControllerBase{
    private readonly IUserServices _userService = userService;

    [HttpGet("{email}", Name = "GetUserByEmail")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetUserByEmail([EmailAddress] string email)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        var user = _userService.GetUserByEmail(email);
        if (user == null)
            return NotFound();
        return Ok(user);
    }


    [HttpGet(Name = "GetAllUsers")]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();

        // Si no hay usuarios, devolver 204 No Content
        if (!users.Any())
            return NoContent();

        // Si hay usuarios, devolver 200 OK con la lista
        return Ok(users);
    }

    [HttpPost(Name = "AddUser")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    public IActionResult AddUser([FromBody] UserDto user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        _userService.AddUser(user);
        return CreatedAtRoute("GetUserByEmail", new { email = user.Email }, user);
    }

    [HttpPut("{email}", Name = "UpdateUser")]
    public IActionResult UpdateUser([FromBody] UserDto user, [FromRoute] string email )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        _userService.UpdateUser(user, email);
        return NoContent();
    }

}