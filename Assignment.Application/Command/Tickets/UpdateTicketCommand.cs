using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Domain.DTO_s.Ticket;
using MediatR;

namespace Assignment.Application.Command.Tickets;

public class UpdateTicketCommand : IRequest<ReadTicketDto>
{
    public int TicketId { get; set; }
    public CreateTicketDto TicketData { get; set; }
}
