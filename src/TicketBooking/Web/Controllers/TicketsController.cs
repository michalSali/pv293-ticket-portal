using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Core.CartAggregate;
using TicketBooking.UseCases.Tickets.AddTicketToCart;
using TicketBooking.UseCases.Tickets.RemoveTicketFromCart;

namespace TicketBooking.Web.Controllers
{
    public class TicketsController : ApiControllerBase
    {
        [HttpPost("/api/tickets")]
        public async Task<ActionResult<Ticket>> AddTicketToCart(AddTicketToCartCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("/api/tickets")]
        public async Task<ActionResult<Ticket>> RemoveTicketFromCart(RemoveTicketFromCartCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
