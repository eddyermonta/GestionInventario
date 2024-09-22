namespace GestionInventario.Controller;

using System.ComponentModel.DataAnnotations;
using GestionInventario.Domain.Dto;
using GestionInventario.Domain.Models;
using GestionInventario.Services;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/[controller]")]
public class UserController(IUserServices userService) : ControllerBase{
    private readonly IUserServices _userService = userService;

    [HttpGet("{email}", Name = "GetUserByEmail")]
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