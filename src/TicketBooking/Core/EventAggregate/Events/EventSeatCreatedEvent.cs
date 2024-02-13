using SharedKernel;

namespace TicketBooking.Core.EventAggregate.Events;

public class EventSeatCreatedEvent : DomainEvent
{
    public int EventId { get; set; }
    public Seat Seat { get; set; }
}
