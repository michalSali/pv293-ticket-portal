using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Models;
using TicketBooking.Core.EventAggregate.Events;

namespace TicketBooking.Core.EventAggregate.Handlers;

public class EventSeatCategoryCreatedHandler : INotificationHandler<DomainEventNotification<EventSeatCategoryCreatedEvent>>
{
    private readonly ILogger<EventCreatedEventHandler> _logger;

    public EventSeatCategoryCreatedHandler(ILogger<EventCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<EventSeatCategoryCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("TicketPortal Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}