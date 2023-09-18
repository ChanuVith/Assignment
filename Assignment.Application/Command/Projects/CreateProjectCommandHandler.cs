using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Application.Abstractions;
using Assignment.Domain.DTO_s.Project;
using Assignment.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Assignment.Application.Command.Projects
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ReadProjectDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ReadProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var projectToCreate = _mapper.Map<Project>(request.ProjectData);
            var createdProject = await _projectRepository.CreateProjectAsync(projectToCreate);
            return _mapper.Map<ReadProjectDto>(createdProject);
        }
    }
}
