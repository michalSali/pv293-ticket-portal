using SharedKernel;

namespace TicketBooking.Core.EventAggregate.Events;

public class EventSeatCategoryCreatedEvent : DomainEvent
{
    public int EventId { get; set; }
    public SeatCategory Category { get; set; }
}
