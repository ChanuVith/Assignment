using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Domain.DTO_s.Ticket;
using MediatR;

namespace Assignment.Application.Query.Ticket
{
    public class GetAllTicketsQuery : IRequest<List<ReadTicketDto>>
    {
        public int? CustomerId { get; set; }
        public int? ProjectId { get; set; }
        public int? UserId { get; set; }
        public string? Status { get; set; }
    }
}
