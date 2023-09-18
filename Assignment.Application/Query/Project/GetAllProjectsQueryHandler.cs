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

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ReadProjectDto>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public GetAllProjectsQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<List<ReadProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetProjectsAsync(request.CustomerId, request.UserId);
        return _mapper.Map<List<ReadProjectDto>>(projects);
    }
}
