using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using UserManagement;
using UserManagement.Core.UserAggregate;
using UserManagement.Core.UserAggregate.Events;

namespace UserManagement.UseCases.Users.Register
{
    internal sealed class RegisterUserHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly UserManagementDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailerService _mailerService;

        public RegisterUserHandler(UserManagementDbContext context, UserManager<ApplicationUser> userManager, IMailerService mailerService)
        {
            _context = context;
            _userManager = userManager;
            _mailerService = mailerService;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser { UserName = request.Email, Email = request.Email };

            var result = await _userManager.CreateAsync(user, request.Password);

            user.DomainEvents.Add(new UserRegisteredEvent
            {
                UserId = user.Id,
                Email = user.Email
            });

            await _mailerService.SendEmailAsync(user.Email, "Welcome to TicketPortal",
                "Thank you for choosing TicketPortal. You can book tickets from events all around the world.");

            return result.Succeeded;
        }
    }
}
