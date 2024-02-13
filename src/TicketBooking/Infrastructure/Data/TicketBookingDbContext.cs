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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);

        // Ignore the DomainEvent entity completely in the database
        builder.Ignore<DomainEvent>();

        builder.ApplyConfiguration(new CartConfiguration());
    }
}