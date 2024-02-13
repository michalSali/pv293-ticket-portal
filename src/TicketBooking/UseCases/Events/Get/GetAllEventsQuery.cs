using MediatR;
using TicketBooking.Core.EventAggregate;

namespace TicketBooking.UseCases.Events.Get
{
    public class GetAllEventsQuery : IRequest<List<Event>>
    {
    }
}
