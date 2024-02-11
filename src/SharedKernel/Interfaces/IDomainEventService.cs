namespace SharedKernel.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}