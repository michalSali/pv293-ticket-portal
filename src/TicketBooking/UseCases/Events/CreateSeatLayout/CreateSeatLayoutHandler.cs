using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Core.EventAggregate;
using TicketBooking.Core.EventAggregate.Events;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.UseCases.Events.CreateSeatLayout
{
    internal sealed class CreateSeatLayoutHandler : IRequestHandler<CreateSeatLayoutCommand, List<Seat>>
    {
        private readonly TicketBookingDbContext _context;

        public CreateSeatLayoutHandler(TicketBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Seat>> Handle(CreateSeatLayoutCommand request, CancellationToken cancellationToken)
        {
            var seats = request.Seats.Select(seat => new Seat
            {
                EventId = request.EventId,
                SectorCode = seat.SectorCode,
                RowNumber = seat.RowNumber,
                SeatNumber = seat.SeatNumber,
                CategoryId = seat.CategoryId,
                State = seat.State ?? SeatState.Available,
            }
            ).ToList();
            
            _context.Seats.AddRange(seats);

            foreach (var seat in _context.Seats)
            {
                seat.DomainEvents.Add(new EventSeatCreatedEvent { EventId = request.EventId, Seat = seat });
            }

            await _context.SaveChangesAsync();

            return seats;
        }
    }
}
