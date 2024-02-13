using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using TicketBooking.Core.EventAggregate;
using TicketBooking.Core.EventAggregate.Events;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.UseCases.Events.CreateSeatCategory
{
    internal sealed class CreateSeatCategoriesHandler : IRequestHandler<CreateSeatCategoryCommand, SeatCategory>
    {
        private readonly TicketBookingDbContext _context;

        public CreateSeatCategoriesHandler(TicketBookingDbContext context)
        {
            _context = context;
        }

        public async Task<SeatCategory> Handle(CreateSeatCategoryCommand request, CancellationToken cancellationToken)
        {
            // check if event with EventId exists

            var category = new SeatCategory
            {
                EventId = request.EventId,
                Name = request.Name,
                Price = request.Price,
            };

            _context.SeatCategories.Add(category);

            category.DomainEvents.Add(new EventSeatCategoryCreatedEvent { EventId = request.EventId, Category = category });

            await _context.SaveChangesAsync();

            return category;
        }
    }
}
