using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Core.EventAggregate;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.UseCases.Events.Get
{
    internal sealed class GetAllEventsHandler : IRequestHandler<GetAllEventsQuery, List<Event>>
    {
        private readonly TicketBookingDbContext _context;

        public GetAllEventsHandler(TicketBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            //var events = await _context.Events
            //    .Include(x => x.Seats)
            //    .ThenInclude(x => x.Category)
            //    .ToListAsync();

            var events = await _context.Events.ToListAsync();
            return events;
        }
    }
}
