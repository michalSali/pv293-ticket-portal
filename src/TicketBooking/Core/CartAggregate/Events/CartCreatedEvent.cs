using SharedKernel;

namespace TicketBooking.Core.CartAggregate.Events;

/// <summary>
/// A domain event that is dispatched whenever a contributor is deleted.
/// The DeleteContributorService is used to dispatch this event.
/// </summary>
public class CartCreatedEvent : DomainEvent
{
    //public TicketAddedToCartEvent(int eventId, int seatId)
    //{

    //}

    public int CartId { get; set; }
    public string UserId { get; set; }
    //public int SeatId { get; }
    //public int CartId { get; }
}
