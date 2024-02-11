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

    [HttpPost("register")]
    public async Task<bool> Register(RegisterUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("sign-in")]
    public async Task<bool> SignIn(SignInUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("log-out")]
    public async Task<bool> LogOut()
    {
        return await Mediator.Send(new LogOutUserCommand());
    }
}
