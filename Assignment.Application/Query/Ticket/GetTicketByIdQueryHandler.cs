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
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, ReadTicketDto>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetTicketByIdQueryHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<ReadTicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(request.TicketId);
            return _mapper.Map<ReadTicketDto>(ticket);
        }
    }
}
