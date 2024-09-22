namespace GestionInventario.Controller;

using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using GestionInventario.Models;
using GestionInventario.Services;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase{
    private readonly IUserServices _userService;

    public UserController(IUserServices userService)
    {
        _userService = userService;
    }

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
    public IActionResult AddUser([FromBody] User user, [FromQuery] string email)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        _userService.AddUser(user, email);
        return CreatedAtRoute("GetUserByEmail", new { email = email }, user);
    }


}