using SharedKernel;

namespace TicketBooking.Core.EventAggregate;

public class Event : AuditableEntity
{
    public int Id { get; set; }

    public string CreatedByUserId { get; set; }

    //public string SessionId { get; set; }
    public virtual IEnumerable<Seat> Seats { get; set; }

    public virtual bool SeatMustBeSpecified => Seats != null && Seats.Any();
}