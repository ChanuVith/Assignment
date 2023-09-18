using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Domain.DTO_s.Project;
using MediatR;

namespace Assignment.Application.Query.Project;

public class GetAllProjectsQuery : IRequest<List<ReadProjectDto>>
{
    public int? CustomerId { get; set; }
    public int? UserId { get; set; }
}
