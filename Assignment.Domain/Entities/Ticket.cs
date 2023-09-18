using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Entities
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? Status { get; set; }

        public int? CustomerId { get; set; }

        public int? ProjectId { get; set; }

        public int? UserId { get; set; }
    }
}
