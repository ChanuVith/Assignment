using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Domain.DTO_s.Ticket;
using MediatR;

namespace Assignment.Application.Query.Ticket
{
    public class GetTicketByIdQuery : IRequest<ReadTicketDto>
    {
        public int TicketId { get; set; } 
    }
}
