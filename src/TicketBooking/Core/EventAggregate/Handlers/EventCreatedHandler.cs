using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Models;
using TicketBooking.Core.EventAggregate.Events;

namespace TicketBooking.Core.EventAggregate.Handlers;

public class EventCreatedEventHandler : INotificationHandler<DomainEventNotification<EventCreatedEvent>>
{
    private readonly ILogger<EventCreatedEventHandler> _logger;

    public EventCreatedEventHandler(ILogger<EventCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<EventCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("TicketPortal Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}