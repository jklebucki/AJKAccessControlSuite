using AJKAccessControl.Shared.DTOs;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AJKAccessGuard.Services
{
    public class UserApiService : IUsersApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto> GetUserAsync(string email)
        {
            var user = await _httpClient.GetFromJsonAsync<UserDto>($"api/account/get-user/{email}");
            if (user == null)
            {
                throw new HttpRequestException("User not found.");
            }
            return user;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(string token)
        {
            var users = await _httpClient.GetFromJsonAsync<IEnumerable<UserDto>>("api/account/get-users");
            return users ?? [];
        }

        public async Task<bool> UpdateUserAsync(UserDto user)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/account/update/{user.Email}", user);
            response.EnsureSuccessStatusCode();
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<bool> DeleteUserAsync(UserDto user)
        {
            var response = await _httpClient.DeleteAsync($"api/users/{user.Email}");
            response.EnsureSuccessStatusCode();
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<bool> RegisterUserAsync(RegisterDto registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/register", registerDto);
            response.EnsureSuccessStatusCode();
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<string> LoginUserAsync(LoginDto loginDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Account/login", loginDto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Login failed.", ex);
            }

        }
    }
}