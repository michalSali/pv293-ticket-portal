using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.UseCases.Carts.GetTickets
{
    internal sealed class GetCartTicketsHandler : IRequestHandler<GetCartTicketsQuery, List<Ticket>>
    {
        private readonly TicketBookingDbContext _context;

        public GetCartTicketsHandler(TicketBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> Handle(GetCartTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _context.Tickets
                .Where(x => x.CartId == request.CartId)
                .ToListAsync();

            return tickets;
        }
    }
}
