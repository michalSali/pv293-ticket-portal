using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.CrossBoundaryEvents;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using UserManagement;
using UserManagement.Core.UserAggregate;
using UserManagement.Core.UserAggregate.Events;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Services.Mailer;

namespace UserManagement.UseCases.Users.Register
{
    internal sealed class RegisterUserHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly UserManagementDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMailerService _mailerService;

        public RegisterUserHandler(UserManagementDbContext context, UserManager<User> userManager, IMailerService mailerService)
        {
            _context = context;
            _userManager = userManager;
            _mailerService = mailerService;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User { UserName = request.Email, Email = request.Email };

            var result = await _userManager.CreateAsync(user, request.Password);

            user.DomainEvents.Add(new UserRegisteredEvent
            {
                UserId = user.Id,
                Email = user.Email
            });

            await _context.SaveChangesAsync();

            await _mailerService.SendEmailAsync(user.Email, null, "Welcome to TicketPortal",
                "Thank you for choosing TicketPortal. You can book tickets from events all around the world.");

            // TODO: need to create Cart for this user

            return result.Succeeded;
        }
    }
}
