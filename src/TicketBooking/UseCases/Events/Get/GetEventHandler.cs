using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Core.EventAggregate;
using TicketPortalArchitecture.Application.Infrastructure.Persistence;

namespace TicketBooking.UseCases.Events.Get
{
    internal sealed class GetEventHandler : IRequestHandler<GetEventQuery, Event>
    {
        private readonly TicketBookingDbContext _context;

        public GetEventHandler(TicketBookingDbContext context)
        {
            _context = context;
        }

        public async Task<Event> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            var _event = await _context.Events
                .Where(x => x.Id == request.EventId)
                .FirstOrDefaultAsync();

            if (_event == null)
            {
                throw new NotFoundException($"Event with id `{request.EventId}` not found");
            }

            return _event;
        }
    }
}
