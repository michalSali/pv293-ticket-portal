using System;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using SharedKernel;
using SharedKernel.Interfaces;

namespace SharedKernel;

public class DomainEventDbContext<T> : DbContext where T : DbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDomainEventService _domainEventService;

    public DbSet<DomainEventLog> EventLogs { get; set; }

    public DomainEventDbContext(
        DbContextOptions<T> options,
        ICurrentUserService currentUserService,
        IDomainEventService domainEventService) : base(options)
    {
        _currentUserService = currentUserService;
        _domainEventService = domainEventService;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.Created = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    break;
            }
        }

        var events = ChangeTracker.Entries<IHasDomainEvent>()
            .Select(x => x.Entity.DomainEvents)
            //.Select(x => x != null)
            .SelectMany(x => x == null ? new List<DomainEvent>() : x)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToArray();

        var entitiesWithEvents = ChangeTracker.Entries<IHasDomainEvent>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any(domainEvent => !domainEvent.IsPublished))
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            foreach (var entry in entity.DomainEvents.Where(x => !x.IsPublished))
            {
                // StreamVersion is auto-increment
                EventLogs.Add(new DomainEventLog
                {
                    StreamId = entity.GetType().Name.ToString(), // entity name
                    StreamVersion = 1,  // should be auto-increment
                    EventType = entry.GetType().Name,
                    EventData = JsonSerializer.Serialize(entry),
                    Timestamp = DateTime.UtcNow,
                });
            }
        }

        var eventLogs = await EventLogs.ToListAsync();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }
}