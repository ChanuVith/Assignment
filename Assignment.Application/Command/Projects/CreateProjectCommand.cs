using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Domain.DTO_s.Project;
using MediatR;

namespace Assignment.Application.Command.Projects
{
    public class CreateProjectCommand : IRequest<ReadProjectDto>
    {
        public CreateProjectDto ProjectData { get; set; }
    }
}
