using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Core.EventAggregate;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.UseCases.Events.GetById
{
    internal sealed class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, Event>
    {
        private readonly TicketBookingDbContext _context;

        public GetEventByIdHandler(TicketBookingDbContext context)
        {
            _context = context;
        }

        public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var _event = await _context.Events
                .Where(x => x.Id == request.EventId)
                .FirstOrDefaultAsync();

            if (_event == null)
            {
                throw new NotFoundException($"Event with id `{request.EventId}` not found");
            }

            var eventSeats = await _context.Seats
                .Where(x => x.EventId == request.EventId)
                .ToListAsync();

            _event.Seats = eventSeats;

            return _event;
        }
    }
}
