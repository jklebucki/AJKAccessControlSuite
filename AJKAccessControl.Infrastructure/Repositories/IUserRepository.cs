using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Domain.Responses; // Ensure this namespace contains UserRepositoryResponse

namespace AJKAccessControl.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<OperationResult> CreateUserAsync(User user, string password);
        Task<OperationResult> DeleteUserAsync(User user);
        Task<OperationResult> CheckPasswordAsync(User user, string password);
        Task<OperationResult> UpdateUserAsync(User user, string password);
        Task<OperationResult> AddUserToRoleAsync(string email, string role);
        Task<IEnumerable<User>> GetUsersAsync();
    }
}