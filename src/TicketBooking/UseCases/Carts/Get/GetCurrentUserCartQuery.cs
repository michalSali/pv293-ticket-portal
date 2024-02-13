using MediatR;
using TicketBooking.Core.CartAggregate;

namespace TicketBooking.UseCases.Carts.Get
{
    public class GetCurrentUserCartQuery : IRequest<Cart>
    {
    }
}
