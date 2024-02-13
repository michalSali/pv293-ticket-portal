using MediatR;
using TicketBooking.Core.CartAggregate;

namespace TicketBooking.UseCases.Carts.Get
{
    public class GetAllCartsQuery : IRequest<List<Cart>>
    {
    }
}
