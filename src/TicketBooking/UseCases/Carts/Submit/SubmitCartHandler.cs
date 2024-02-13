using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Core.EventAggregate;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.UseCases.Carts.Submit
{
    internal sealed class SubmitCartHandler : IRequestHandler<SubmitCartCommand, Cart>
    {
        private readonly TicketBookingDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public SubmitCartHandler(TicketBookingDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Cart> Handle(SubmitCartCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException("Current user not set.");  // more of a bad request
            }

            var cart = await _context.Carts
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                throw new NotFoundException($"Cart for user with id `{userId}` not found");
            }

            // TODO:
            // set state to submitted, adjust seats state, create order

            return cart;
        }
    }
}
