using MediatR;
using TicketBooking.Core.CartAggregate;

namespace TicketBooking.UseCases.Carts.GetById
{
    public class GetCartByIdQuery : IRequest<Cart>
    {
        public int CartId { get; set; }
    }
}
