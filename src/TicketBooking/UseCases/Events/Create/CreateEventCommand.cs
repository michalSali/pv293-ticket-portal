using MediatR;
using TicketBooking.Core.EventAggregate;

namespace TicketBooking.UseCases.Events.Create
{
    public class CreateEventCommand : IRequest<Event>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public EventCategory Category { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }

        public decimal? Fee { get; set; } // e.g. for online events where price is not tied to a seat
        public bool HasUnlimitedCapacity { get; set; } // e.g. for online events
        public int TotalCapacity { get; set; }
        public int RemainingCapacity { get; set; }
    }
}
