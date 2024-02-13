using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Core.CartAggregate;
using TicketBooking.UseCases.Carts.Get;
using TicketBooking.UseCases.Carts.GetById;
using TicketBooking.UseCases.Tickets.AddTicketToCart;
using TicketBooking.UseCases.Tickets.RemoveTicketFromCart;

namespace TicketBooking.Web.Controllers
{
    public class CartsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Cart>>> GetAllCarts()
        {
            return await Mediator.Send(new GetAllCartsQuery());
        }

        [HttpGet("current-user")]
        public async Task<ActionResult<Cart>> GetCurrentUserCart()
        {
            return await Mediator.Send(new GetCurrentUserCartQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCartById(int id)
        {
            return await Mediator.Send(new GetCartByIdQuery { CartId = id });
        }

    }
}
