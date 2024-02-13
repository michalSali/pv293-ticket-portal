using System.Reflection;

using FluentValidation;

using MediatR;
using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Behaviours;
using UserManagement.Infrastructure.Services.Mailer;
using UserManagement.Core.UserAggregate;
using Microsoft.AspNetCore.Identity;
using UserManagement.Infrastructure.Data;

namespace UserManagement;

public static class DependencyInjection
{

    public static IServiceCollection AddUserManagementServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddUserManagementInfrastructure(configuration);

        services.AddIdentity<User, IdentityRole>(options =>
        {
            // Configure password requirements
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 1; // Set the minimum password length
        })
        .AddRoleManager<RoleManager<IdentityRole>>()
        .AddDefaultUI()
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<UserManagementDbContext>();

        services.AddSingleton<IMailerService, IdentityMailerService>();

        return services;
    }


    private static IServiceCollection AddUserManagementInfrastructure(this IServiceCollection services, IConfiguration configuration)
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

        var configurationBuilder = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configurationBuilder["POSTGRES_MASTER_CONN"];

        connectionString = "Host=localhost;Port=5432;Database=ticketportal-master;Username=postgres;Password=password";
        //connectionString = "Host=localhost;Port=5432;Database=ticketportal-master;Username=postgres;Password=test";
        //connectionString = "Host=localhost;Port=5432;Database=ticketportal-master;Username=test;Password=password";

        services.AddDbContext<UserManagementDbContext>(options =>
            //options.UseNpgsql(connectionString));
            options.UseInMemoryDatabase("TicketPortalDb"));


        return services;
    }
}