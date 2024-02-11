using TicketBooking.Core.CartAggregate;

namespace TicketPortalArchitecture.Application.Infrastructure.Persistence;

public static class TicketBookingDbContextSeed
{
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