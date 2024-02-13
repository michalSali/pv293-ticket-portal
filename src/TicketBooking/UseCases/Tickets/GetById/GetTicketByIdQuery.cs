using MediatR;
using TicketBooking.Core.CartAggregate;

namespace TicketBooking.UseCases.Tickets.GetById
{
    public class GetTicketByIdQuery : IRequest<Ticket>
    {
        public int TicketId { get; set; }
    }
}
