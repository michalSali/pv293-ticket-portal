using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Models;
using UserManagement.Core.UserAggregate.Events;

namespace UserManagement.Core.UserAggregate.Handlers;

public class UserRegisteredEventHandler : INotificationHandler<DomainEventNotification<UserRegisteredEvent>>
{
    private readonly ILogger<UserRegisteredEventHandler> _logger;
    private readonly UserManagementDbContext _context;

    public UserRegisteredEventHandler(ILogger<UserRegisteredEventHandler> logger, UserManagementDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(DomainEventNotification<UserRegisteredEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        var eventName = domainEvent.GetType().Name;

        _logger.LogInformation($"TicketPortal Domain Event: {eventName}, " +
            $"Details: UserId = {domainEvent.UserId}, Email = {domainEvent.Email}");
    }
}