using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using SharedKernel.CrossBoundaryEvents;
using SharedKernel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Core.EventAggregate;
using TicketBooking.UseCases.Events.Create;
using TicketBooking.UseCases.Events.CreateSeatCategory;
using TicketBooking.UseCases.Events.CreateSeatLayout;
using TicketBooking.UseCases.Events.Get;
using TicketBooking.UseCases.Events.GetById;

namespace TicketBooking.Web.Controllers
{
    public class EventLogsController : ApiControllerBase
    {
        [HttpGet("ticket-booking")]
        public async Task<ActionResult<List<DomainEventLog>>> GetEventLog()
        {
            return await Mediator.Send(new GetEventLogsQuery());
        }
    }
}
