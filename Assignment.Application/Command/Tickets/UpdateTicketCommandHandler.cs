using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Application.Abstractions;
using Assignment.Domain.DTO_s.Ticket;
using AutoMapper;
using MediatR;

namespace Assignment.Application.Command.Tickets;

public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, ReadTicketDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public UpdateTicketCommandHandler(ITicketRepository ticketRepository, IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public async Task<ReadTicketDto> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var existingTicket = await _ticketRepository.GetTicketByIdAsync(request.TicketId);

        _mapper.Map(request.TicketData, existingTicket);

        var updatedTicket = await _ticketRepository.UpdateTicketAsync(existingTicket);
        return _mapper.Map<ReadTicketDto>(updatedTicket);
    }
}
