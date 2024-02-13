using System.Reflection;

using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;
using TicketBooking.UseCases.Tickets.AddTicketToCart;
using UserManagement;
using UserManagement.UseCases.Users.Register;

namespace TicketBooking;

public static class DependencyInjection
{

    public static IServiceCollection AddTicketPortalServices(this IServiceCollection services, IConfiguration configuration)
    {
        // https://stackoverflow.com/questions/75848218/no-service-for-type-mediatr-irequesthandler-has-been-registred-net-6
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // registers assemblies for each project
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(RegisterUserCommand).Assembly,
            typeof(AddTicketToCartCommand).Assembly
        ));

        // TODO: add validator for each project
        //services.AddValidatorsFromAssemblies(Assembly.GetExecutingAssembly());

        services.AddSharedKernelServices(configuration);
        services.AddTicketBookingServices(configuration);
        services.AddUserManagementServices(configuration);

        return services;
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
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