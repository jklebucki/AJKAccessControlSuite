using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs; // Assuming UserDto is in this namespace

namespace AJKAccessControl.Application.Services
{
    public interface IUserService
    {
        Task<OperationResult<string>> RegisterUserAsync(RegisterUserDto registerDto);
        Task<OperationResult<string>> LoginUserAsync(LoginDto loginDto);
        Task<OperationResult<string>> DeleteUserAsync(DeleteUserDto deleteUserDto);
        Task<OperationResult<string>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<OperationResult<string>> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<UserDto> GetUserAsync(string email);
        Task<OperationResult<string>> AddUserToRoleAsync(AddUserToRoleDto addRoleDto);
        Task<IEnumerable<UserDto>> GetUsersAsync();
    }
}