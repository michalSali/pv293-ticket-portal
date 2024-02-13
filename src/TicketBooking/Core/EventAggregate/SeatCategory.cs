using SharedKernel;
using System.Text.Json.Serialization;

namespace TicketBooking.Core.EventAggregate;

public class SeatCategory : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    [JsonIgnore]
    public virtual List<Seat> Seats { get; } = new List<Seat>();

    public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();
}