using MediatR;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Core.EventAggregate;

namespace TicketBooking.UseCases.Carts.Submit
{
    public class SubmitCartCommand : IRequest<Cart>
    {
    }
}
