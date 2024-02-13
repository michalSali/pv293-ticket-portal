using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Core.CartAggregate;
using TicketBooking.Core.EventAggregate;
using TicketBooking.UseCases.Events.Create;
using TicketBooking.UseCases.Events.Get;
using TicketBooking.UseCases.Events.GetById;

namespace TicketBooking.Web.Controllers
{
    public class EventsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            return await Mediator.Send(new GetAllEventsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            return await Mediator.Send(new GetEventByIdQuery() { EventId = id });
        }

        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(CreateEventCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
