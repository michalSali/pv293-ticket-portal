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
    internal sealed class GetAllCartsHandler : IRequestHandler<GetAllCartsQuery, List<Cart>>
    {
        private readonly TicketBookingDbContext _context;

        public GetAllCartsHandler(TicketBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cart>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
        {
            // include tickets
            var carts = await _context.Carts.ToListAsync();
            return carts;
        }
    }
}
