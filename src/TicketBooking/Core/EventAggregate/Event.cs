using SharedKernel;
using System.Text.Json.Serialization;

namespace TicketBooking.Core.EventAggregate;

public class Event : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }
    
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

    public virtual IEnumerable<Seat> Seats { get; set; }

    [JsonIgnore]
    public virtual bool SeatMustBeSpecified => Seats != null && Seats.Any();

    public List<DomainEvent> DomainEvents { get; set; }
}