using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.CrossBoundaryEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement;
using UserManagement.Core.UserAggregate;

namespace UserManagement.Infrastructure.Data;

/// <summary>
/// Provides initial values for lookup lists, for demo purposes.
/// </summary>
public static class UserManagementDbContextSeed
{
    /// <summary>
    /// Data seeding is performed before the main application logic begins execution.
    /// </summary>
    /// <param name="serviceProvider">Services</param>
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<UserManagementDbContext>();
        //context.Database.EnsureCreated();
        //context.Database.Migrate();

        var hasher = new PasswordHasher<User>();

        var userID1 = Guid.NewGuid().ToString();
        var userID2 = Guid.NewGuid().ToString();
        var userID3 = Guid.NewGuid().ToString();
        var userID4 = Guid.NewGuid().ToString();
        var userID5 = Guid.NewGuid().ToString();
        var userID6 = Guid.NewGuid().ToString();
        var userID7 = Guid.NewGuid().ToString();
        var userID8 = Guid.NewGuid().ToString();
        var userID9 = Guid.NewGuid().ToString();

        var user1 = new User() { Id = userID1, UserName = "485342_user1@mailinator.com", NormalizedUserName = "485342_USER1@MAILINATOR.COM", Email = "485342_user1@mailinator.com", NormalizedEmail = "485342_USER9@MAILINA1OR.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "Heslo.1"), SecurityStamp = string.Empty, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, Firstname = "Joe", Lastname = "Foo" };
        var user2 = new User() { Id = userID2, UserName = "485342_user2@mailinator.com", NormalizedUserName = "485342_USER2@MAILINATOR.COM", Email = "485342_user2@mailinator.com", NormalizedEmail = "485342_USER2@MAILINATOR.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "Heslo.1"), SecurityStamp = string.Empty, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, Firstname = "Bob", Lastname = "Doe" };
        var user3 = new User() { Id = userID3, UserName = "485342_user3@mailinator.com", NormalizedUserName = "485342_USER3@MAILINATOR.COM", Email = "485342_user3@mailinator.com", NormalizedEmail = "485342_USER3@MAILINATOR.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "Heslo.1"), SecurityStamp = string.Empty, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, Firstname = "Beth", Lastname = "Jeff" };
        var user4 = new User() { Id = userID4, UserName = "485342_user4@mailinator.com", NormalizedUserName = "485342_USER4@MAILINATOR.COM", Email = "485342_user4@mailinator.com", NormalizedEmail = "485342_USER4@MAILINATOR.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "Heslo.1"), SecurityStamp = string.Empty, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, Firstname = "John", Lastname = "Fabrikam" };

        var users = new List<User>()
        {
            user1,
            user2,
            user3,
            user4,
        };

        if (!context.ApplicationUsers.Any())
        {
            //context.ApplicationUsers.Add(entity: new ApplicationUser() { Id = userID9, UserName = "485342_user9@mailinator.com", NormalizedUserName = "485342_USER9@MAILINATOR.COM", Email = "485342_user9@mailinator.com", NormalizedEmail = "485342_USER9@MAILINATOR.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "Heslo.1"), SecurityStamp = string.Empty, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, Firstname = "Alex", Lastname = "Bohm" });
            //context.ApplicationUsers.Add(entity: new ApplicationUser() { Id = userID8, UserName = "485342_user8@mailinator.com", NormalizedUserName = "485342_USER8@MAILINATOR.COM", Email = "485342_user8@mailinator.com", NormalizedEmail = "485342_USER8@MAILINATOR.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "Heslo.1"), SecurityStamp = string.Empty, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, Firstname = "Alyssa", Lastname = "Wilson" });
            //context.ApplicationUsers.Add(entity: new ApplicationUser() { Id = userID7, UserName = "485342_user7@mailinator.com", NormalizedUserName = "485342_USER7@MAILINATOR.COM", Email = "485342_user7@mailinator.com", NormalizedEmail = "485342_USER7@MAILINATOR.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "Heslo.1"), SecurityStamp = string.Empty, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, Firstname = "Nina", Lastname = "Nerdy" });
            //context.ApplicationUsers.Add(entity: new ApplicationUser() { Id = userID6, UserName = "485342_user6@mailinator.com", NormalizedUserName = "485342_USER6@MAILINATOR.COM", Email = "485342_user6@mailinator.com", NormalizedEmail = "485342_USER6@MAILINATOR.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "Heslo.1"), SecurityStamp = string.Empty, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, Firstname = "Jill", Lastname = "Anderson" });
            //context.ApplicationUsers.Add(entity: new ApplicationUser() { Id = userID5, UserName = "485342_user5@mailinator.com", NormalizedUserName = "485342_USER5@MAILINATOR.COM", Email = "485342_user5@mailinator.com", NormalizedEmail = "485342_USER5@MAILINATOR.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "Heslo.1"), SecurityStamp = string.Empty, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, Firstname = "David", Lastname = "Miller" });

            context.ApplicationUsers.AddRange(users);

            foreach (var user in users)
            {
                user.DomainEvents.Add(new UserRegisteredEvent() { UserId = user.Id, Email = user.Email });
            }

            await context.SaveChangesAsync();
        }

        if (!context.Roles.Any())
        {
            context.Roles.Add(entity: new IdentityRole() { Id = 3.ToString(), Name = "EventOrganizer", NormalizedName = "EventOrganizer" });
            context.Roles.Add(entity: new IdentityRole() { Id = 2.ToString(), Name = "User", NormalizedName = "USER" });
            context.Roles.Add(entity: new IdentityRole() { Id = 1.ToString(), Name = "Admin", NormalizedName = "ADMIN" });

            await context.SaveChangesAsync();
        }

        if (!context.UserRoles.Any())
        {
            context.UserRoles.Add(entity: new IdentityUserRole<string>() { UserId = userID4, RoleId = 2.ToString() });
            context.UserRoles.Add(entity: new IdentityUserRole<string>() { UserId = userID3, RoleId = 3.ToString() });
            context.UserRoles.Add(entity: new IdentityUserRole<string>() { UserId = userID2, RoleId = 2.ToString() });
            context.UserRoles.Add(entity: new IdentityUserRole<string>() { UserId = userID1, RoleId = 1.ToString() });

            await context.SaveChangesAsync();
        }

        if (!context.RoleClaims.Any())
        {
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 3.ToString(), ClaimType = "permission", ClaimValue = "Events.Create" });
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 3.ToString(), ClaimType = "permission", ClaimValue = "Events.Edit" });
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 3.ToString(), ClaimType = "permission", ClaimValue = "Events.Remove" });
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 3.ToString(), ClaimType = "permission", ClaimValue = "Events.View" });
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 3.ToString(), ClaimType = "permission", ClaimValue = "Events.ViewAll" });

            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 3.ToString(), ClaimType = "permission", ClaimValue = "Events.View" });
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 3.ToString(), ClaimType = "permission", ClaimValue = "Events.ViewAll" });

            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 2.ToString(), ClaimType = "permission", ClaimValue = "Tickets.Create" });
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 2.ToString(), ClaimType = "permission", ClaimValue = "Webhostings.Create" });
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 2.ToString(), ClaimType = "permission", ClaimValue = "Webhostings.View" });
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 1.ToString(), ClaimType = "permission", ClaimValue = "Webhostings.Remove" });
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 1.ToString(), ClaimType = "permission", ClaimValue = "Webhostings.Duplicate" });
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 1.ToString(), ClaimType = "permission", ClaimValue = "Webhostings.Edit" });
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 1.ToString(), ClaimType = "permission", ClaimValue = "Webhostings.Create" });
            context.RoleClaims.Add(entity: new IdentityRoleClaim<string>() { RoleId = 1.ToString(), ClaimType = "permission", ClaimValue = "Webhostings.View" });

            await context.SaveChangesAsync();
        }
    }
 }