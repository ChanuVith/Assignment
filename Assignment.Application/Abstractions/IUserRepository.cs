using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Domain.DTO_s.User;
using Assignment.Domain.Entities;

namespace Assignment.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync(int? customerId = null, int? projectId = null);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> CreateUserAsync(User user);
    }
}
