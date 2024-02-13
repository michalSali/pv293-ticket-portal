using MediatR;
using UserManagement.Core.UserAggregate;

namespace UserManagement.UseCases.Users.LogOut
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
    }
}
