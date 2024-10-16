using AJKAccessControl.Shared.DTOs;

namespace AJKAccessGuard.Services
{
    public interface IUsersApiService
    {
        Task<bool> RegisterUserAsync(RegisterDto registerDto);
        Task<string> LoginUserAsync(LoginDto loginDto); 
        Task<UserDto> GetUserAsync(string email, string token);
        Task<IEnumerable<UserDto>> GetAllUsersAsync(string token);
        Task<bool> UpdateUserAsync(UserDto user);
        Task<bool> DeleteUserAsync(UserDto user);
    }
}