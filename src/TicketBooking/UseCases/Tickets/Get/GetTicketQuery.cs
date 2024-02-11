using MediatR;
using TicketBooking.Core.CartAggregate;

namespace TicketBooking.UseCases.Tickets.Get
{
    public class GetTicketQuery : IRequest<Ticket>
    {
        public int TicketId { get; set; }
    }
}
