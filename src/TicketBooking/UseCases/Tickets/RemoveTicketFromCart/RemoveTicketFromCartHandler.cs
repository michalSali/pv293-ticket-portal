using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Core.CartAggregate.Events;
using TicketPortalArchitecture.Application.Infrastructure.Persistence;

namespace TicketBooking.UseCases.Tickets.RemoveTicketFromCart
{
    internal sealed class RemoveTicketFromCartHandler : IRequestHandler<RemoveTicketFromCartCommand, Ticket>
    {
        private readonly TicketBookingDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public RemoveTicketFromCartHandler(TicketBookingDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Ticket> Handle(RemoveTicketFromCartCommand request, CancellationToken cancellationToken)
        {            
            var ticket = await _context.Tickets
                .Where(x => x.Id == request.TicketId)
                .FirstOrDefaultAsync();
            
            if (ticket == null)
            {
                throw new NotFoundException($"Ticket with id `{request.TicketId}` not found");
            }

            // TODO: make seat available

            _context.Tickets.Remove(ticket);

            ticket.DomainEvents.Add(new TicketRemovedFromCartEvent());

            await _context.SaveChangesAsync(cancellationToken);

            return ticket;
        }
    }
}
