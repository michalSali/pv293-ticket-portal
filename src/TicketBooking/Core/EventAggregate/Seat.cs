using SharedKernel;

namespace TicketBooking.Core.EventAggregate;

public class Seat : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string? UserId { get; set; }

    public string SectorCode { get; set; }
    public int RowNumber { get; set; }
    public int SeatNumber { get; set; }

    public int CategoryId { get; set; }
    public SeatState State { get; set; }

    public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();

    public virtual SeatCategory Category { get; set; }
}