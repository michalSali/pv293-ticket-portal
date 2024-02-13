using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using TicketBooking.Infrastructure.Data;
using TicketBooking.Core.CartAggregate;

namespace TicketBooking.UseCases.Carts.Get
{
    internal sealed class GetCurrentUserCartHandler : IRequestHandler<GetCurrentUserCartQuery, Cart>
    {
        private readonly TicketBookingDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetCurrentUserCartHandler(TicketBookingDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Cart> Handle(GetCurrentUserCartQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new NotFoundException("Current user not found.");
            }

            var cart = await _context.Carts
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                throw new NotFoundException($"Cart not found for user with id `{userId}`.");

            }

            return cart;
        }
    }
}
