using MediatR;
using TicketBooking.Core.EventAggregate;

namespace TicketBooking.UseCases.Events.GetById
{
    public class GetEventByIdQuery : IRequest<Event>
    {
        public int EventId { get; set; }
    }
}
