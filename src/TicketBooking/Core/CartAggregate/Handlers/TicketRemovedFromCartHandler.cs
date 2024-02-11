using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Models;
using TicketBooking.Core.CartAggregate.Events;

namespace TicketBooking.Core.CartAggregate.Handlers;

public class TicketRemovedFromCartEventHandler : INotificationHandler<DomainEventNotification<TicketRemovedFromCartEvent>>
{
    private readonly ILogger<TicketRemovedFromCartEventHandler> _logger;

    public TicketRemovedFromCartEventHandler(ILogger<TicketRemovedFromCartEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<TicketRemovedFromCartEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("TicketPortal Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}