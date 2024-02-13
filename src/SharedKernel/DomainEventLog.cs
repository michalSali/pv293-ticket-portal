using System.ComponentModel.DataAnnotations.Schema;

namespace SharedKernel;


public class DomainEventLog
{
    public int Id { get; set; } // should be UUID ?
    public string StreamId { get; set; } // name of entity/aggregate
    public int StreamVersion { get; set; } // order number of event (e.g. the first event related to "Cart" is "CartCreatedEvent"
    public string EventType { get; set; }
    public string EventData { get; set; }
    public DateTime Timestamp { get; set; }
}