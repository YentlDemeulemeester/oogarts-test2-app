using Microsoft.AspNetCore.Mvc;

namespace Oogarts.Server.Controllers.Users;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    // private readonly UserService _userService;

    // public UserController(UserService userService)
    // {
    //     _userService = userService;
    // }

    // [HttpGet]
    // public IActionResult GetAllUsers()
    // {
    //     var users = _userService.GetAllUsers();
    //     return Ok(users);
    // }

    // [HttpGet("{id}")]
    // public IActionResult GetUser(int id)
    // {
    //     var user = _userService.GetUserById(id);
    //     if (user == null)
    //     {
    //         return NotFound();
    //     }
    //     return Ok(user);
    // }

    // [HttpPost]
    // public IActionResult CreateUser(User user)
    // {
    //     _userService.CreateUser(user);
    //     return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
    // }
}
