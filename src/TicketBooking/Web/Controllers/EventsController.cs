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
using TicketBooking.UseCases.Events.CreateSeatCategory;
using TicketBooking.UseCases.Events.CreateSeatLayout;
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
            var result = await Mediator.Send(new GetEventByIdQuery() { EventId = id });
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(CreateEventCommand command)
        {
            var _event = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetEvent), new {id = _event.Id}, _event);
        }

        [HttpPost("{id}/seat-layout")]
        public async Task<ActionResult<List<Seat>>> CreateEventSeatLayout(int id, List<SeatDto> seats)
        {
            var command = new CreateSeatLayoutCommand
            {
                EventId = id,
                Seats = seats
            };
            return await Mediator.Send(command);
        }

        [HttpPost("{id}/seat-category")]
        public async Task<ActionResult<SeatCategory>> CreateEventSeatCategories(int id, CreateSeatCategoryCommand command)
        {
            command.EventId = id;
            return await Mediator.Send(command);
        }
    }
}
