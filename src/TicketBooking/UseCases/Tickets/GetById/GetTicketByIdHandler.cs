﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.UseCases.Tickets.GetById
{
    internal sealed class GetTicketByIdHandler : IRequestHandler<GetTicketByIdQuery, Ticket>
    {
        private readonly TicketBookingDbContext _context;

        public GetTicketByIdHandler(TicketBookingDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets
                .Where(x => x.Id == request.TicketId)
                .FirstOrDefaultAsync();

            if (ticket == null)
            {
                throw new NotFoundException($"Ticket with id `{request.TicketId}` not found");
            }

            return ticket;
        }
    }
}
