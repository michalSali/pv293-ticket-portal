using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using SharedKernel.CrossBoundaryEvents;
using SharedKernel.Queries;

namespace UserManagement.Web.Controllers
{
    public class EventLogsController : ApiControllerBase
    {
        [HttpGet("user-management")]
        public async Task<ActionResult<List<DomainEventLog>>> GetEventLog()
        {
            return await Mediator.Send(new GetEventLogsQuery());
        }
    }
}
