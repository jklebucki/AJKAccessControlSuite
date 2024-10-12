using AJKAccessControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AJKAccessControl.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string userId);
    }
}