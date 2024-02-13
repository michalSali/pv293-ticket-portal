using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Core.CartAggregate.Events;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.UseCases.Tickets.AddTicketToCart
{
    internal sealed class AddTicketToCartHandler : IRequestHandler<AddTicketToCartCommand, Ticket>
    {
        private readonly TicketBookingDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public AddTicketToCartHandler(TicketBookingDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Ticket> Handle(AddTicketToCartCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var cart = await _context.Carts
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                throw new NotFoundException($"Cart for user with id `{userId}` not found");
            }

            var _event = await _context.Events
                    .Where(x => x.Id == request.EventId)
                    .Include(x => x.Seats)
                    .FirstOrDefaultAsync();

            if (_event == null)
            {
                throw new NotFoundException($"Event with id `{request.EventId}` not found");
            }

            if (_event.SeatMustBeSpecified && request.SeatId.HasValue)
            {
                var alreadyExistsTicketWithSeat = await _context.Tickets
                    .Where(x => x.SeatId == request.SeatId)
                    .AnyAsync();

                if (alreadyExistsTicketWithSeat)
                {
                    throw new InvalidOperationException($"There already exists ticket for seat `{request.SeatId}` (eventId: {request.EventId})");
                }
            }
            
            if (_event.SeatMustBeSpecified && !request.SeatId.HasValue)
            {
                // if seat is not set, and event has seats, than throw an exception
                throw new InvalidOperationException("Event with id `` must have seat specified.");
            }

            var ticket = new Ticket
            {
                EventId = request.EventId,
                CartId = cart.Id,
            };

            _context.Tickets.Add(ticket);

            ticket.DomainEvents.Add(new TicketAddedToCartEvent());

            await _context.SaveChangesAsync(cancellationToken);

            return ticket;
        }
    }
}
