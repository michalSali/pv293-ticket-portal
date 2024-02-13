using MediatR;
using TicketBooking.Core.EventAggregate;

namespace TicketBooking.UseCases.Events.CreateSeatCategory
{
    public class CreateSeatCategoryCommand : IRequest<SeatCategory>
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
