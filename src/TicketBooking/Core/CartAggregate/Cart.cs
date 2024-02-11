using SharedKernel;

namespace TicketBooking.Core.CartAggregate;

public class Cart : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }

    public string UserId { get; set; }

    //public string SessionId { get; set; }
    public virtual IEnumerable<Ticket> Tickets { get; set; }

    public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();
}