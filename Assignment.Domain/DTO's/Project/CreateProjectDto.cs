using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.DTO_s.Project
{
    public class CreateProjectDto
    {
        public string Name { get; set; }
        public int? CustomerId { get; set; }
        public int? UserId { get; set; }
    }
}
