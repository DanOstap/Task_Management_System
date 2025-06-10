using Microsoft.AspNetCore.Mvc;
using Task_Management_System.Models;
using Task_Management_System.Service;

namespace Task_Management_System.Controllers;

[Route("api/usrs")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUsersService usersService;

    public UserController(IUsersService usersService)
    {
        this.usersService = usersService;
    }
    [HttpPost]
    async public Task<ActionResult<Users>> CreateUser(  [FromBody] Users UserDto)
    {
        var NewUser = await usersService.CreateUsers(UserDto);
        return NewUser;
    }

    [HttpGet("/by/email/{email}")]
    async public Task<ActionResult<Users>> FindUserByEmail( [FromHeader] string email)
    {
        var User = await usersService.FindUsersOneByEmail(email);
        if (User != null) return User;
        return NotFound();
    }

    [HttpGet("/by/link/{activationLink}")]
    async public Task<ActionResult<Users>> FindUserByLink( [FromRoute] string activationLink)
    {
        var  User  = await usersService.FindOneUserByActivationLink(activationLink);
        if (User != null) return User;
        return NotFound();
    }

    [HttpGet("/all")]
    async public Task<IActionResult> GetUsers()
    {
        var Users = await usersService.FindAllUsers();
        if (Users != null) return Ok(Users);
        return NotFound();
    }

    [HttpGet("/by/{id}")]
    async public Task<ActionResult<Users>> GetUserById( [FromRoute] int id)
    {
        var User = await usersService.FindOneUserById(id);
        if (User != null) return Ok(User);
        return NotFound();
    }

    [HttpPut("/by/{id}")]
    async public Task<ActionResult<Users>> UpdateUser([FromRoute] int id,[FromBody] Users UserDto)
    {
        var UpdateUser = await usersService.FindOneUserById(id);
        if (UpdateUser != null) return UpdateUser;
        return NotFound();
    }

    [HttpDelete("/by/{id}")]
    async public Task<ActionResult<Users>> DeleteUser([FromRoute]  int id)
    {
        var deletteUser = await usersService.FindOneUserById(id);
        return  deletteUser;
    }
}