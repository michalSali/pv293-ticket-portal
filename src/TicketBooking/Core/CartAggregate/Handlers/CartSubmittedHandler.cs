using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Models;
using TicketBooking.Core.CartAggregate.Events;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.Core.CartAggregate.Handlers;

public class CartSubmittedEventHandler : INotificationHandler<DomainEventNotification<CartSubmittedEvent>>
{
    private readonly ILogger<CartSubmittedEventHandler> _logger;
    private readonly TicketBookingDbContext _context;

    public CartSubmittedEventHandler(ILogger<CartSubmittedEventHandler> logger, TicketBookingDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(DomainEventNotification<CartSubmittedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("TicketPortal Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        await _context.SaveChangesAsync();
    }
}