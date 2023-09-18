using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Application.Abstractions;
using Assignment.Domain.DTO_s.User;
using Assignment.Domain.Entities;
using Assignment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetUsersAsync(int? customerId = null, int? projectId = null)
        {
            var query = _dbContext.User.AsQueryable();

            if (customerId.HasValue)
            {
                query = query.Where(u => u.CustomerId == customerId);
            }

            if (projectId.HasValue)
            {
                query = query.Where(u => u.ProjectId == projectId);
            }

            return await query.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _dbContext.User.FindAsync(userId);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _dbContext.User.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }


}

