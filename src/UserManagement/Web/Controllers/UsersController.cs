using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Threading.Tasks;
using UserManagement.Core.UserAggregate;
using UserManagement.UseCases.Users.LogOut;
using UserManagement.UseCases.Users.Register;
using UserManagement.UseCases.Users.SignIn;

namespace UserManagement.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ApiControllerBase
{
    public UsersController() { }

    [HttpPost("registration")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var user = await Mediator.Send(command);

        if (user == null)
        {
            return BadRequest("Registration wasn't succesful.");
        }

        // TODO: ideally return 201 CreatedAtAction() with created user's id
        return Ok(user);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInUserCommand command)
    {
        var user = await Mediator.Send(command);

        if (user == null)
        {
            return BadRequest("Registration wasn't succesful.");
        }

        return Ok(user);
    }

    [HttpPost("log-out")]
    public async Task<bool> LogOut()
    {
        return await Mediator.Send(new LogOutUserCommand());
    }

    [HttpGet]
    public async Task<List<User>> GetAllUsers()
    {
        return await Mediator.Send(new GetAllUsersQuery());
    }
}
