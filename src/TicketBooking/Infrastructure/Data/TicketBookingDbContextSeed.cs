using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Core.EventAggregate;

namespace TicketBooking.Infrastructure.Data;

public static class TicketBookingDbContextSeed
{
    /// <summary>
    /// Data seeding is performed before the main application logic begins execution.
    /// </summary>
    /// <param name="serviceProvider">Services</param>
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<TicketBookingDbContext>();
        //context.Database.EnsureCreated();
        //context.Database.Migrate();

        if (context.Events.Any())
        {
            return;
        }

        var event_1 = new Event
        {
            Title = "ABC Concert",
            Description = "Concert of the group ABC",
            Category = EventCategory.Music,
            Start = new DateTime(2024, 3, 5),
            Location = "Brno, Street 13/37",
            Url = "www.google.com",
            HasUnlimitedCapacity = false,
            TotalCapacity = 5, // should match number of seats
            RemainingCapacity = 5
        };

        var event_2 = new Event
        {
            Title = "Clean code & Vertical slicing",
            Description = "Clean code & Vertical slicing architecture presentation to pass PV293",
            Category = EventCategory.Online,
            Start = new DateTime(2024, 3, 5),
            End = new DateTime(2024, 3, 7),
            Location = "Brno, Street 13/37",
            Url = "www.google.com",
            HasUnlimitedCapacity = true,
            Fee = 20
        };

        context.Events.AddRange(new List<Event> { event_1, event_2 });

        var seatCategory_11 = new SeatCategory() { Name = "Tier 1", Price = 119 };
        var seatCategory_12 = new SeatCategory() { Name = "Tier 2", Price = 99 };
        var seatCategory_13 = new SeatCategory() { Name = "Tier 3", Price = 79 };

        var seatCategory_21 = new SeatCategory() { Name = "VIP Lounge", Price = 249 };
        var seatCategory_22 = new SeatCategory() { Name = "Balcony A", Price = 99 };
        var seatCategory_23 = new SeatCategory() { Name = "Balcony B", Price = 99 };
        var seatCategory_24 = new SeatCategory() { Name = "Main Floor", Price = 149 };

        var seatCategories = new List<SeatCategory>
        {
            seatCategory_11,
            seatCategory_12,
            seatCategory_13,
            seatCategory_21,
            seatCategory_22,
            seatCategory_23,
            seatCategory_24,
        };

        context.SeatCategories.AddRange(seatCategories);

        var seat_1 = new Seat { SectorCode = "A", RowNumber = 1, SeatNumber = 1, CategoryId = seatCategory_11.Id,  EventId = event_1.Id, State = SeatState.Available };
        var seat_2 = new Seat { SectorCode = "A", RowNumber = 1, SeatNumber = 2, CategoryId = seatCategory_11.Id, EventId = event_1.Id, State = SeatState.Available };
        var seat_3 = new Seat { SectorCode = "A", RowNumber = 1, SeatNumber = 3, CategoryId = seatCategory_11.Id, EventId = event_1.Id, State = SeatState.Available };
        var seat_4 = new Seat { SectorCode = "B1", RowNumber = 2, SeatNumber = 1, CategoryId = seatCategory_12.Id, EventId = event_1.Id, State = SeatState.Available };
        var seat_5 = new Seat { SectorCode = "B2", RowNumber = 3, SeatNumber = 1, CategoryId = seatCategory_12.Id, EventId = event_1.Id, State = SeatState.Available };
        var seat_6 = new Seat { SectorCode = "VIP", RowNumber = 1, SeatNumber = 1, CategoryId = seatCategory_13.Id, EventId = event_1.Id, State = SeatState.Available };
        
        context.Seats.AddRange(new List<Seat> { seat_1, seat_2, seat_3, seat_4, seat_5, seat_6 });

        context.SaveChanges();
    }

    public static async Task SeedSampleDataAsync(TicketBookingDbContext context)
    {
        // Seed, if necessary
        if (!context.Carts.Any())
        {
            // create carts (set user ids)
            context.Carts.Add(new Cart
            {
                
            });

            await context.SaveChangesAsync();
        }
    }
}