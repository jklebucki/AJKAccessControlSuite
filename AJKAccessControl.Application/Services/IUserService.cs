using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs; // Assuming UserDto is in this namespace

namespace AJKAccessControl.Application.Services
{
    public interface IUserService
    {
        Task<OperationResult> RegisterUserAsync(RegisterDto registerDto);
        Task<OperationResult> LoginUserAsync(LoginDto loginDto);
        Task<OperationResult> DeleteUserAsync(DeleteUserDto deleteUserDto);
        Task<OperationResult> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<OperationResult> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<UserDto> GetUserAsync(string email);
        Task<OperationResult> AddUserToRoleAsync(AddUserToRoleDto addRoleDto);
        Task<IEnumerable<UserDto>> GetUsersAsync();
    }
}