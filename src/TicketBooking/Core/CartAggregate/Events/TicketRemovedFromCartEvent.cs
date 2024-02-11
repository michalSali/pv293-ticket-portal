using SharedKernel;

namespace TicketBooking.Core.CartAggregate.Events;

/// <summary>
/// A domain event that is dispatched whenever a contributor is deleted.
/// The DeleteContributorService is used to dispatch this event.
/// </summary>
public class TicketRemovedFromCartEvent : DomainEvent
{
    //public TicketAddedToCartEvent(int eventId, int seatId)
    //{
        
    //}

    //public int EventId { get; }
    //public int SeatId { get; }
    //public int CartId { get; }
}
