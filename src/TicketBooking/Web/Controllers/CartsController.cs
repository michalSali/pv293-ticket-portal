using CartManagement.UseCases.Carts.LogOut;
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
    public class CartsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Cart>>> GetAllCarts()
        {
            return await Mediator.Send(new GetAllCartsQuery());
        }

    }
}
