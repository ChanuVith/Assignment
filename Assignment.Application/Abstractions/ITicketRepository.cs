using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Domain.Entities;

namespace Assignment.Application.Abstractions
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetTicketsAsync(int? customerId, int? projectId, int? userId, string? status);
        Task<Ticket> GetTicketByIdAsync(int ticketId);
        Task<Ticket> CreateTicketAsync(Ticket ticket);
        Task<Ticket> UpdateTicketAsync(Ticket ticket);
    }
}
