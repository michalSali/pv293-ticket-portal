using MediatR;
using TicketBooking.Core.EventAggregate;

namespace TicketBooking.UseCases.Events.Get
{
    public class GetEventQuery : IRequest<Event>
    {
        public int EventId { get; set; }
    }
}
