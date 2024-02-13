using SharedKernel;

namespace SharedKernel.CrossBoundaryEvents;

/// <summary>
/// A domain event that is dispatched whenever a contributor is deleted.
/// The DeleteContributorService is used to dispatch this event.
/// </summary>
public class UserRegisteredEvent : DomainEvent
{
    public string UserId { get; set; }
    public string Email { get; set; }
}