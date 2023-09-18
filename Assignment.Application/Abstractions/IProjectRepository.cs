using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Domain.Entities;

namespace Assignment.Application.Abstractions
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetProjectsAsync(int? customerId = null, int? userId = null);
        Task<Project> GetProjectByIdAsync(int projectId);
        Task<Project> CreateProjectAsync(Project project);
        Task<Project> UpdateProjectAsync(Project project);
    }
}
