using System.Reflection;

using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TicketPortalArchitecture.Application.Common.Behaviours;
using TicketPortalArchitecture.Application.Common.Interfaces;
using TicketPortalArchitecture.Application.Infrastructure.Files;
using TicketPortalArchitecture.Application.Infrastructure.Persistence;
using TicketPortalArchitecture.Application.Infrastructure.Services;

namespace TicketBooking;

public static class DependencyInjection
{

    public static IServiceCollection AddBoundedContextsServices(this IServiceCollection services)
    {

    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TicketPortalDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IDomainEventService, DomainEventService>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationContext>();

        return services;
    }
}