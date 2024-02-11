using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Models;
using TicketBooking.Core.CartAggregate.Events;

namespace TicketBooking.Core.CartAggregate.Handlers;

public class TicketAddedToCartEventHandler : INotificationHandler<DomainEventNotification<TicketAddedToCartEvent>>
{
    private readonly ILogger<TicketAddedToCartEventHandler> _logger;

    public TicketAddedToCartEventHandler(ILogger<TicketAddedToCartEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<TicketAddedToCartEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("TicketPortal Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}