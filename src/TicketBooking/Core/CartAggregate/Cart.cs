using SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TicketBooking.Core.CartAggregate;

//public class Cart : AuditableEntity, IHasDomainEvent, ICachedEntity
public class Cart : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public virtual IEnumerable<Ticket> Tickets { get; set; }

    public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();

    [NotMapped]
    [JsonIgnore]
    public virtual string CacheKey => $"{nameof(Cart)}_{nameof(Id)}-{Id}";
}