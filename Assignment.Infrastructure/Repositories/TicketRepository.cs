using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Application.Abstractions;
using Assignment.Domain.Entities;
using Assignment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context; // Replace with your DbContext class

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetTicketsAsync(int? customerId, int? projectId, int? userId, string? status)
        {
            var query = _context.Ticket.AsQueryable();

            if (customerId.HasValue)
            {
                query = query.Where(ticket => ticket.CustomerId == customerId);
            }

            if (projectId.HasValue)
            {
                query = query.Where(ticket => ticket.ProjectId == projectId);
            }

            if (userId.HasValue)
            {
                query = query.Where(ticket => ticket.UserId == userId);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(ticket => ticket.Status == status);
            }

            return await query.ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Ticket.FindAsync(ticketId);
        }

        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket> UpdateTicketAsync(Ticket ticket)
        {
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ticket;
        }
    }
}
