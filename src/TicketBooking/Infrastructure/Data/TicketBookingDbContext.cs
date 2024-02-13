using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using SharedKernel;
using SharedKernel.Interfaces;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Core.EventAggregate;
using TicketBooking.Infrastructure.Data.Configurations;

namespace TicketBooking.Infrastructure.Data;

public class TicketBookingDbContext : DomainEventDbContext<TicketBookingDbContext>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDomainEventService _domainEventService;

    public TicketBookingDbContext(
        DbContextOptions<TicketBookingDbContext> options,
        ICurrentUserService currentUserService,
        IDomainEventService domainEventService) : base(options, currentUserService, domainEventService)
    {
        _currentUserService = currentUserService;
        _domainEventService = domainEventService;
    }

    public DbSet<Cart> Carts { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<Event> Events { get; set; }

    public DbSet<Seat> Seats { get; set; }

    public DbSet<SeatCategory> SeatCategories { get; set; }

    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    //{
    //    foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
    //    {
    //        switch (entry.State)
    //        {
    //            case EntityState.Added:
    //                entry.Entity.CreatedBy = _currentUserService.UserId;
    //                entry.Entity.Created = DateTime.UtcNow;
    //                break;
    //            case EntityState.Modified:
    //                entry.Entity.LastModifiedBy = _currentUserService.UserId;
    //                entry.Entity.LastModified = DateTime.UtcNow;
    //                break;
    //            case EntityState.Detached:
    //                break;
    //            case EntityState.Unchanged:
    //                break;
    //            case EntityState.Deleted:
    //                break;
    //            default:
    //                break;
    //        }
    //    }

    //    var events = ChangeTracker.Entries<IHasDomainEvent>()
    //            //.Select(x => x.Entity.DomainEvents)
    //            //.SelectMany(x => x)
    //            //.Where(domainEvent => !domainEvent.IsPublished)
    //            .ToArray();

    //    var result = await base.SaveChangesAsync(cancellationToken);

    //    //await DispatchEvents(events);

    //    return result;
    //}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);

        // Ignore the DomainEvent entity completely in the database
        builder.Ignore<DomainEvent>();

        builder.ApplyConfiguration(new CartConfiguration());
    }

    //private async Task DispatchEvents(DomainEvent[] events)
    //{
    //    foreach (var @event in events)
    //    {
    //        @event.IsPublished = true;
    //        await _domainEventService.Publish(@event);
    //    }
    //}
}