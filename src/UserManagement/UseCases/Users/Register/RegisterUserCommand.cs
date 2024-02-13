using MediatR;
using UserManagement.Core.UserAggregate;

namespace UserManagement.UseCases.Users.Register
{
    public class RegisterUserCommand : IRequest<User?>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
