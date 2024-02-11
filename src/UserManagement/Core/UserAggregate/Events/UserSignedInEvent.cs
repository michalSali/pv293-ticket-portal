using SharedKernel;

namespace UserManagement.Core.UserAggregate.Events
{
    public class UserSignedInEvent : DomainEvent
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
