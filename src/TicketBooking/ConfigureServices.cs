using System.Reflection;

using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking;

public static class DependencyInjection
{

    public static IServiceCollection AddTicketBookingServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTicketBookingInfrastructure(configuration);

        return services;
    }

    private static IServiceCollection AddTicketBookingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configurationBuilder["POSTGRES_MASTER_CONN"];

        connectionString = "Host=localhost;Port=5432;Database=ticketportal-master;Username=postgres;Password=password";
        connectionString = "Host=host.docker.internal;Port=5432;Database=ticketportal-master;Username=postgres;Password=password";
        connectionString = "Host=sali-intra-test-db.postgres.database.azure.com;Port=5432;Database=ticketportal;Username=ticketportal_user;Password=password";
        //connectionString = "Host=localhost;Port=5432;Database=ticketportal-master;Username=test;Password=password";

        services.AddDbContext<TicketBookingDbContext>(options =>
            //options.UseNpgsql(connectionString));
            options.UseInMemoryDatabase("TicketPortalDb"));

        // TODO: add redis cache

        //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        //{
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseInMemoryDatabase("TicketPortalDb"));
        //}
        //else
        //{
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseSqlServer(
        //            configuration.GetConnectionString("DefaultConnection"),
        //            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        //}

        return services;
    }
}