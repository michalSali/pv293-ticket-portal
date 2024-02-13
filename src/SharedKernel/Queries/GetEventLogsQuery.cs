using MediatR;

namespace SharedKernel.Queries
{
    public class GetEventLogsQuery : IRequest<List<DomainEventLog>>
    {
    }
}
