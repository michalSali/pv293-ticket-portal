using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Models;
using TicketBooking.Core.CartAggregate.Events;
using TicketPortalArchitecture.Application.Infrastructure.Persistence;

namespace TicketBooking.Core.CartAggregate.Handlers;

public class UserCreatedEventHandler : INotificationHandler<DomainEventNotification<UserCreatedEvent>>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;
    private readonly TicketBookingDbContext _context;

    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger, TicketBookingDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(DomainEventNotification<UserCreatedEvent> notification, CancellationToken cancellationToken)
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