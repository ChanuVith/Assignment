using Assignment.Application.Command.Tickets;
using Assignment.Application.Query.Ticket;
using Assignment.Domain.DTO_s.Ticket;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<List<ReadTicketDto>>> GetTickets([FromQuery] GetAllTicketsQuery query)
        {
            var tickets = await _mediator.Send(query);
            return Ok(tickets);
        }

        [HttpGet("{id}"), Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<ReadTicketDto>> GetTicketById(int id)
        {
            var query = new GetTicketByIdQuery { TicketId = id };
            var ticket = await _mediator.Send(query);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<ActionResult<ReadTicketDto>> CreateTicket([FromBody] CreateTicketDto ticketData)
        {
            var command = new CreateTicketCommand { TicketData = ticketData };
            var createdTicket = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTicketById), new { id = createdTicket.TicketId }, createdTicket);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReadTicketDto>> UpdateTicket(int id, [FromBody] CreateTicketDto ticketData)
        {
            var command = new UpdateTicketCommand { TicketId = id, TicketData = ticketData };
            var updatedTicket = await _mediator.Send(command);

            if (updatedTicket == null)
            {
                return NotFound();
            }

            return Ok(updatedTicket);
        }
    }
}
