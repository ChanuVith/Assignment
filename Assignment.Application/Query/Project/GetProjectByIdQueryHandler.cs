using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Application.Abstractions;
using Assignment.Domain.DTO_s.Project;
using AutoMapper;
using MediatR;

namespace Assignment.Application.Query.Project;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ReadProjectDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectByIdQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<ReadProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetProjectByIdAsync(request.ProjectId);
        return _mapper.Map<ReadProjectDto>(project);
    }
}
