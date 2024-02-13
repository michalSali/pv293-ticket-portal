using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using UserManagement;
using UserManagement.Core.UserAggregate;
using UserManagement.Core.UserAggregate.Events;

namespace UserManagement.UseCases.Users.LogOut
{
    internal sealed class LogOutUserHandler : IRequestHandler<LogOutUserCommand, bool>
    {
        private readonly IDomainEventService _domainEventService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserService _currentUserService;

        public LogOutUserHandler(
            IDomainEventService domainEventService,
            IHttpContextAccessor httpContextAccessor,
            ICurrentUserService currentUserService)
        {
            _domainEventService = domainEventService;
            _httpContextAccessor = httpContextAccessor;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(LogOutUserCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                return false;
            }

            var userId = _currentUserService.UserId;
            var email = _currentUserService.Email;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email))
            {
                throw new NotFoundException("Current user not set");
            }

            await httpContext.SignOutAsync();

            await _domainEventService.Publish(new UserLoggedOutEvent
            {
                UserId = userId,
                Email = email
            });

            var logOutSuccessful = _currentUserService.User == null;
            return logOutSuccessful;
        }
    }
}
