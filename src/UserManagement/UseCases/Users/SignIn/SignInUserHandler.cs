using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using UserManagement;
using UserManagement.Core.UserAggregate;
using UserManagement.Core.UserAggregate.Events;
using UserManagement.Infrastructure.Data;

namespace UserManagement.UseCases.Users.SignIn
{
    internal sealed class SignInUserHandler : IRequestHandler<SignInUserCommand, bool>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IDomainEventService _domainEventService;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManagementDbContext _context;


        public SignInUserHandler(
            SignInManager<User> signInManager,
            IDomainEventService domainEventService,
            ICurrentUserService currentUserService,
            UserManagementDbContext context)
        {
            _signInManager = signInManager;
            _domainEventService = domainEventService;
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<bool> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return false;
            }

            var user = await _context.Users
                .Where(x => x.Email == request.Email)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException($"User with email `{request.Email}` not found");
            }

            await _domainEventService.Publish(new UserSignedInEvent
            {
                Email = request.Email,
                UserId = user.Id
            });

            return true;
        }
    }
}
