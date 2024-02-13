using MediatR;
using TicketBooking.Core.EventAggregate;

namespace TicketBooking.UseCases.Events.Create
{
    public class CreateEventCommand : IRequest<Event>
    {
        public int EventId { get; set; }
    }
}
