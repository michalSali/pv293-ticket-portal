using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using UserManagement;
using UserManagement.Core.UserAggregate;
using UserManagement.Core.UserAggregate.Events;

namespace UserManagement.UseCases.Users.SignIn
{
    internal sealed class SignInUserHandler : IRequestHandler<SignInUserCommand, bool>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDomainEventService _domainEventService;
        private readonly ICurrentUserService _currentUserService;


        public SignInUserHandler(
            SignInManager<ApplicationUser> signInManager,
            IDomainEventService domainEventService,
            ICurrentUserService currentUserService)
        {
            _signInManager = signInManager;
            _domainEventService = domainEventService;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, lockoutOnFailure: false);

            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException("Current user not set");
            }

            await _domainEventService.Publish(new UserSignedInEvent
            {
                Email = request.Email,
                UserId = userId
            });

            return result.Succeeded;
        }
    }
}
