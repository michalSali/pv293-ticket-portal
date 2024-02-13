using MediatR;
using UserManagement.Core.UserAggregate;

namespace UserManagement.UseCases.Users.SignIn
{
    public class SignInUserCommand : IRequest<User?>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
