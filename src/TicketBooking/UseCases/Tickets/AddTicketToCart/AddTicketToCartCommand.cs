using MediatR;
using TicketBooking.Core.CartAggregate;

namespace TicketBooking.UseCases.Tickets.AddTicketToCart
{
    public class AddTicketToCartCommand : IRequest<Ticket>
    {
        public int EventId { get; set; }

        // if the ticket is for online events,
        // there is not seat specified
        public int? SeatId { get; set; }
    }
}
