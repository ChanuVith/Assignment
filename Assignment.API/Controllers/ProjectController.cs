using Assignment.Application.Command.Projects;
using Assignment.Application.Query.Project;
using Assignment.Domain.DTO_s.Project;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult<List<ReadProjectDto>>> GetProjects([FromQuery] GetAllProjectsQuery query)
        {
            var projects = await _mediator.Send(query);
            return Ok(projects);
        }

        [HttpGet("{id}"), Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult<ReadProjectDto>> GetProjectById(int id)
        {
            var query = new GetProjectByIdQuery { ProjectId = id };
            var project = await _mediator.Send(query);
            return Ok(project);
        }

        [HttpPost, Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<ReadProjectDto>> CreateProject([FromBody] CreateProjectDto projectData)
        {
            var command = new CreateProjectCommand { ProjectData = projectData };
            var createdProject = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.ProjectId }, createdProject);
        }

        [HttpPut("{id}"), Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<ReadProjectDto>> UpdateProject(int id, [FromBody] CreateProjectDto projectData)
        {
            var command = new UpdateProjectCommand { ProjectId = id, ProjectData = projectData };
            var updatedProject = await _mediator.Send(command);
            return Ok(updatedProject);
        }
        
    }
}
