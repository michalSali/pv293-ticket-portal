using MediatR;
using TicketBooking.Core.CartAggregate;

namespace TicketBooking.UseCases.Carts.GetTickets
{
    public class GetCartTicketsQuery : IRequest<List<Ticket>>
    {
        public int CartId { get; set; }
    }
}
