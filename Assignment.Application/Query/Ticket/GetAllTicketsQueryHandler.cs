using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Application.Abstractions;
using Assignment.Domain.DTO_s.Ticket;
using AutoMapper;
using MediatR;

namespace Assignment.Application.Query.Ticket
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, List<ReadTicketDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetAllTicketsQueryHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<List<ReadTicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketRepository.GetTicketsAsync(request.CustomerId, request.ProjectId, request.UserId, request.Status);
            return _mapper.Map<List<ReadTicketDto>>(tickets);
        }
    }
}
