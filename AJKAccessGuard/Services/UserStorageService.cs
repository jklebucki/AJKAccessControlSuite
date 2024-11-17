using AJKAccessControl.Shared.DTOs;
using Microsoft.JSInterop;

namespace AJKAccessGuard.Services;
public class UserStorageService : IUserStorageService
{
    private readonly IJSRuntime _jsRuntime;
    private UserDto _userDto = new();
    private string _token = string.Empty;

    public UserStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task StoreUserAsync(UserDto userDto)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userName", userDto.UserName);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "email", userDto.Email);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "phoneNumber", userDto.PhoneNumber);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "firstName", userDto.FirstName);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "lastName", userDto.LastName);
    }

    public async Task<UserDto> GetUserAsync()
    {
        //var accessToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "accessToken");
        var userName = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userName");
        var email = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "email");
        var phoneNumber = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "phoneNumber");
        var firstName = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "firstName");
        var lastName = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "lastName");
        return new UserDto
        {
            UserName = userName,
            Email = email,
            PhoneNumber = phoneNumber,
            FirstName = firstName,
            LastName = lastName
        };
    }

    public async Task StoreTokenAsync(string token)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "accessToken", token);
    }

    public async Task<string?> GetTokenAsync()
    {
        var token = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "accessToken");
        if (token == "null")
        {
            return string.Empty;
        }
        else
        {
            return token;
        }
    }

    public async Task ClearStorage()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "accessToken");
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userName");
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "email");
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "phoneNumber");
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "firstName");
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "lastName");
    }
}