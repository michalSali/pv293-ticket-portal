using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Queries;
using UserManagement.Core.UserAggregate;
using UserManagement.Infrastructure.Data;

namespace UserManagement.UseCases.Users.Get
{
    internal sealed class GetEventLogsHandler : IRequestHandler<GetUserEventLogsQuery, List<UserEventLog>>
    {
        private readonly UserManagementDbContext _context;

        public GetEventLogsHandler(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserEventLog>> Handle(GetUserEventLogsQuery request, CancellationToken cancellationToken)
        {
            var eventLogs = await _context.UserEventLogs.ToListAsync();
            return eventLogs;
        }
    }
}
