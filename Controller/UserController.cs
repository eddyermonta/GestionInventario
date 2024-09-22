namespace GestionInventario.Controller;

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
    public IActionResult GetUserByEmail(string email)
    {
        var user = _userService.GetUserByEmail(email);

        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }


    [HttpGet(Name = "GetAllUsers")]
    public IActionResult GetAllUsers()
    {
        return Ok(_userService.GetAllUsers());
    }

    [HttpPost(Name = "AddUser")]
    public IActionResult AddUser(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _userService.AddUser(user);
        return CreatedAtRoute("GetUserByEmail", new { email = user.DocumentNumber }, user);
    }


}