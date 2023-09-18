﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.DTO_s.Ticket
{
    public class CreateTicketDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int? CustomerId { get; set; }

        public int? ProjectId { get; set; }

        public int? UserId { get; set; }
    }
}
