using SharedKernel;

namespace TicketBooking.Core.CartAggregate.Events;

/// <summary>
/// A domain event that is dispatched whenever a contributor is deleted.
/// The DeleteContributorService is used to dispatch this event.
/// </summary>
public class CartSubmittedEvent : DomainEvent
{
    public Cart Cart { get; set; }
}
