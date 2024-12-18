using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Domain.Responses; // Ensure this namespace contains UserRepositoryResponse

namespace AJKAccessControl.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserNameAsync(string userName);
        Task<OperationResult<string>> CreateUserAsync(User user, string password, string role);
        Task<OperationResult<string>> DeleteUserAsync(User user);
        Task<OperationResult<string>> CheckPasswordAsync(User user, string password);
        Task<OperationResult<string>> UpdateUserAsync(string userName, User user);
        Task<OperationResult<string>> AddUserToRoleAsync(string userName, string role);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<OperationResult<string>> ChangePasswordAsync(User user, string password);
    }
}