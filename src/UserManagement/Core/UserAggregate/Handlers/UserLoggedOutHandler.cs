using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Models;
using UserManagement.Core.UserAggregate.Events;

namespace UserManagement.Core.UserAggregate.Handlers;

public class UserLoggedOutEventHandler : INotificationHandler<DomainEventNotification<UserLoggedOutEvent>>
{
    private readonly ILogger<UserLoggedOutEventHandler> _logger;

    public UserLoggedOutEventHandler(ILogger<UserLoggedOutEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(DomainEventNotification<UserLoggedOutEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        var eventName = domainEvent.GetType().Name;

        _logger.LogInformation($"TicketPortal Domain Event: {eventName}, " +
            $"Details: UserId = {domainEvent.UserId}, Email = {domainEvent.Email}");
    }
}