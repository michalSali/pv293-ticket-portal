using MediatR;
using TicketBooking.Core.CartAggregate;

namespace CartManagement.UseCases.Carts.LogOut
{
    public class GetAllCartsQuery : IRequest<List<Cart>>
    {
    }
}
