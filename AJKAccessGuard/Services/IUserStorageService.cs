using AJKAccessControl.Shared.DTOs;

namespace AJKAccessGuard.Services;
public interface IUserStorageService
{
    Task StoreUserAsync(UserDto userDto);
    Task<UserDto> GetUserAsync();
    Task StoreTokenAsync(string token);
    Task<string> GetTokenAsync();
    Task ClearStorage();
}