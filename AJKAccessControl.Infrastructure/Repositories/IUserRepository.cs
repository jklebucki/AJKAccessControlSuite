using AJKAccessControl.Domain.Entities;

namespace AJKAccessControl.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> CreateUserAsync(User user, string password);
        Task<bool> DeleteUserAsync(User user);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<bool> UpdateUserAsync(User user, string password);
        Task<bool> AddUserToRoleAsync(string email, string role);
    }
}