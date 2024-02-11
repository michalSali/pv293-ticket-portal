using MediatR;

namespace UserManagement.UseCases.Users.Register
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
