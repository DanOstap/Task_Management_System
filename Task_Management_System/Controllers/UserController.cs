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
        return Ok(NewUser);
    }

    [HttpGet("/by/email/{email}")]
    async public Task<ActionResult<Users>> FindUserByEmail( [FromHeader] string email)
    {
        var User = await usersService.FindUsersOneByEmail(email);
        if (User != null) return Ok(User);
        return NotFound();
    }

    [HttpGet("/by/link/{activationLink}/")]
    async public Task<ActionResult<Users>> FindUserByLink( [FromHeader] string activationLink)
    {
        var  User  = await usersService.FindOneUserByActivationLink(activationLink);
        if (User != null) return Ok(User);
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
    async public Task<ActionResult<Users>> GetUserById( [FromHeader] int id)
    {
        var User = await usersService.FindOneUserById(id);
        if (User != null) return Ok(User);
        return NotFound();
    }

    [HttpPut("{id}")]
    async public Task<IActionResult> UpdateUser([FromHeader] int id,[FromBody] Users UserDto)
    {
        var UpdateUser = await usersService.FindOneUserById(id);
        if (UpdateUser != null) return Ok(UpdateUser);
        return NotFound();
    }

    [HttpDelete("{id}")]
    async public Task<IActionResult> DeleteUser([FromHeader]  int id)
    {
        var deletteUser = await usersService.FindOneUserById(id);
        
        return  Ok(deletteUser);
    }
}