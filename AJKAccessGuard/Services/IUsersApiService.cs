using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessGuard.Services
{
    public interface IUsersApiService
    {
        Task<OperationResult<string>> RegisterUserAsync(RegisterUserDto registerDto, string token);
        Task<OperationResult<string>> LoginUserAsync(LoginDto loginDto);
        Task<OperationResult<UserDto>> GetUserAsync(string userName, string token);
        Task<OperationResult<IEnumerable<UserDto>>> GetAllUsersAsync(string token);
        Task<OperationResult<string>> UpdateUserAsync(string userName, UpdateUserDto user, string token);
        Task<OperationResult<string>> DeleteUserAsync(UserDto user, string token);
        Task<OperationResult<string>> ChangePasswordAsync(ChangePasswordDto changePasswordDto, string token);
    }
}