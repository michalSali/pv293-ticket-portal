using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Queries;
using TicketBooking.Core.EventAggregate;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.UseCases.Events.Get
{
    internal sealed class GetEventLogsHandler : IRequestHandler<GetEventLogsQuery, List<DomainEventLog>>
    {
        private readonly TicketBookingDbContext _context;

        public GetEventLogsHandler(TicketBookingDbContext context)
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
