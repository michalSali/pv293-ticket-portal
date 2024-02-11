using System.Security.Claims;

namespace SharedKernel.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Email { get; }
    ClaimsPrincipal? User { get; }
}