using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Application.Abstractions;
using Assignment.Domain.DTO_s.Ticket;
using Assignment.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Assignment.Application.Command.Tickets;

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, ReadTicketDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public CreateTicketCommandHandler(ITicketRepository ticketRepository, IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public async Task<ReadTicketDto> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticketToCreate = _mapper.Map<Ticket>(request.TicketData);
        var createdTicket = await _ticketRepository.CreateTicketAsync(ticketToCreate);
        return _mapper.Map<ReadTicketDto>(createdTicket);
    }
}
