using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Application.Abstractions;
using Assignment.Domain.DTO_s.Project;
using AutoMapper;
using MediatR;

namespace Assignment.Application.Command.Projects
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ReadProjectDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ReadProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var existingProject = await _projectRepository.GetProjectByIdAsync(request.ProjectId);

            _mapper.Map(request.ProjectData, existingProject);

            var updatedProject = await _projectRepository.UpdateProjectAsync(existingProject);
            return _mapper.Map<ReadProjectDto>(updatedProject);
        }
    }
}
