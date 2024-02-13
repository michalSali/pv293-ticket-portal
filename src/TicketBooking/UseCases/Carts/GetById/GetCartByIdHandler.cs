using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.UseCases.Carts.GetById
{
    internal sealed class GetCartByIdHandler : IRequestHandler<GetCartByIdQuery, Cart>
    {
        private readonly TicketBookingDbContext _context;

        public GetCartByIdHandler(TicketBookingDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Where(x => x.Id == request.CartId)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                throw new NotFoundException($"Cart with id `{request.CartId}` not found.");
            }

            // use .Include once properly set via FKs
            var tickets = await _context.Tickets
                .Where(x => x.CartId == request.CartId)
                .ToListAsync();

            cart.Tickets = tickets;

            return cart;
        }
    }
}
