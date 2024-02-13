using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Queries;
using UserManagement.Infrastructure.Data;

namespace UserManagement.UseCases.Users.Get
{
    internal sealed class GetEventLogsHandler : IRequestHandler<GetEventLogsQuery, List<DomainEventLog>>
    {
        private readonly UserManagementDbContext _context;

        public GetEventLogsHandler(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<DomainEventLog>> Handle(GetEventLogsQuery request, CancellationToken cancellationToken)
        {
            var eventLogs = await _context.EventLogs.ToListAsync();
            return eventLogs;
        }
    }
}
