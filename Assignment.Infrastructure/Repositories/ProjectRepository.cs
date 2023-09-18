using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Application.Abstractions;
using Assignment.Domain.Entities;
using Assignment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetProjectsAsync(int? customerId = null, int? userId = null)
        {
            IQueryable<Project> query = _context.Project;

            if (customerId.HasValue)
            {
                query = query.Where(p => p.CustomerId == customerId);
            }

            if (userId.HasValue)
            {
                query = query.Where(p => p.UserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            return await _context.Project.FindAsync(projectId);
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            _context.Project.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return project;
        }
    }
}
