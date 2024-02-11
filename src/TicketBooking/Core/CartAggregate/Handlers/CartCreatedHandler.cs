using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Models;
using TicketBooking.Core.CartAggregate.Events;
using TicketPortalArchitecture.Application.Infrastructure.Persistence;

namespace TicketBooking.Core.CartAggregate.Handlers;

public class CartCreatedEventHandler : INotificationHandler<DomainEventNotification<CartCreatedEvent>>
{
    private readonly ILogger<CartCreatedEventHandler> _logger;
    private readonly TicketBookingDbContext _context;

    public CartCreatedEventHandler(ILogger<CartCreatedEventHandler> logger, TicketBookingDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(DomainEventNotification<CartCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("TicketPortal Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        await _context.SaveChangesAsync();
    }
}