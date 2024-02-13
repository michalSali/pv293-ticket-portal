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
using UserManagement.Infrastructure.Data;

namespace UserManagement.UseCases.Users.LogOut
{
    internal sealed class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly UserManagementDbContext _context;

        public GetAllUsersHandler(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
    }
}
