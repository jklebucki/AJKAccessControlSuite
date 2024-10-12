using AJKAccessControl.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AJKAccessControl.Application.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string userId);
    }
}