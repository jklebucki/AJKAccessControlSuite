using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Domain.Responses; // Ensure this namespace contains UserRepositoryResponse

namespace AJKAccessControl.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<OperationResult<string>> CreateUserAsync(User user, string password);
        Task<OperationResult<string>> DeleteUserAsync(User user);
        Task<OperationResult<string>> CheckPasswordAsync(User user, string password);
        Task<OperationResult<string>> UpdateUserAsync(User user, string password);
        Task<OperationResult<string>> AddUserToRoleAsync(string email, string role);
        Task<IEnumerable<User>> GetUsersAsync();
    }
}