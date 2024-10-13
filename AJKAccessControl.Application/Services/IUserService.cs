using AJKAccessControl.Shared.DTOs; // Assuming UserDto is in this namespace

namespace AJKAccessControl.Application.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterDto registerDto);
        Task<string> LoginUserAsync(LoginDto loginDto);
        Task<bool> DeleteUserAsync(DeleteUserDto deleteUserDto);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<bool> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<UserDto> GetUserAsync(string email);
        Task<bool> AddUserToRoleAsync(AddUserToRoleDto addRoleDto);
    }
}