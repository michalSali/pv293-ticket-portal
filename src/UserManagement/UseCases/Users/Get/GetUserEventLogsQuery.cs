using MediatR;
using UserManagement.Core.UserAggregate;

namespace UserManagement.UseCases.Users.Get
{
    public class GetUserEventLogsQuery : IRequest<List<UserEventLog>>
    {
    }
}
