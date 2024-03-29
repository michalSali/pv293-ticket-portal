﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Core.EventAggregate;
using TicketBooking.Core.EventAggregate.Events;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.UseCases.Events.Create
{
    internal sealed class CreateEventHandler : IRequestHandler<CreateEventCommand, Event>
    {
        private readonly TicketBookingDbContext _context;

        public CreateEventHandler(TicketBookingDbContext context)
        {
            _context = context;
        }

        public async Task<Event> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var _event = new Event
            {
                Title = request.Title,
                Description = request.Description,
                Category = request.Category,
                Start = request.Start,
                End = request.End,
                Location = request.Location,
                Url = request.Url,
                Fee = request.Fee,
                HasUnlimitedCapacity = request.HasUnlimitedCapacity,
                TotalCapacity = request.TotalCapacity,
                RemainingCapacity = request.RemainingCapacity,
            };

            _context.Events.Add(_event);

            _event.DomainEvents.Add(new EventCreatedEvent());

            await _context.SaveChangesAsync();

            return _event;
        }
    }
}
