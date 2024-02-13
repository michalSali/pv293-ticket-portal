using Microsoft.AspNetCore.Identity;
using SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UserManagement.Core.UserAggregate
{
    public class User : IdentityUser, IHasDomainEvent
    {
        public virtual string? Firstname { get; set; }
        public virtual string? Lastname { get; set; }
        public virtual string? PersonalEmail { get; set; }
        public virtual string? BusinessPhone { get; set; }

        public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();
    }
}
