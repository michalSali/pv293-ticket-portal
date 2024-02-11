using MediatR;
using TicketBooking.Core.CartAggregate;

namespace TicketBooking.UseCases.Tickets.RemoveTicketFromCart
{
    public class RemoveTicketFromCartCommand : IRequest<Ticket>
    {
        public int TicketId { get; set; }
    }
}
