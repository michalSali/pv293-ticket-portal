using SharedKernel;

namespace TicketBooking.Core.CartAggregate;

public class Ticket : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int CartId { get; set; }
    public int? SeatId { get; set; }

    public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();
}