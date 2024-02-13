using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.CrossBoundaryEvents;
using SharedKernel.Models;
using TicketBooking.Core.CartAggregate.Events;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.Core.CartAggregate.Handlers;

public class UserCreatedEventHandler : INotificationHandler<DomainEventNotification<UserRegisteredEvent>>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;
    private readonly TicketBookingDbContext _context;

    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger, TicketBookingDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(DomainEventNotification<UserRegisteredEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("TicketPortal Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        var cart = new Cart
        {
            UserId = domainEvent.UserId
        };

        _context.Carts.Add(cart);
        cart.DomainEvents.Add(new CartCreatedEvent
        {
            CartId = cart.Id,
            UserId = cart.UserId
        });
        await _context.SaveChangesAsync();
    }
}