using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessControl.Application.Services
{
    public interface IUserService
    {
        Task<OperationResult<string>> RegisterUserAsync(RegisterUserDto registerDto);
        Task<OperationResult<string>> LoginUserAsync(LoginDto loginDto);
        Task<OperationResult<string>> DeleteUserAsync(DeleteUserDto deleteUserDto);
        Task<OperationResult<string>> ChangePasswordAsync(ChangePasswordDto forgotPasswordDto);
        Task<OperationResult<string>> UpdateUserAsync(string userName, UpdateUserDto updateUserDto);
        Task<UserDto> GetUserAsync(string email);
        Task<OperationResult<string>> AddUserToRoleAsync(AddUserToRoleDto addRoleDto);
        Task<IEnumerable<UserDto>> GetUsersAsync();
    }
}