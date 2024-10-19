using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace AJKAccessGuard.Services
{
    public class UserApiService : IUsersApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OperationResult<UserDto>> GetUserAsync(string userName, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var user = await _httpClient.GetFromJsonAsync<UserDto>($"api/account/get-user/{userName}");
            if (user == null)
            {
                throw new HttpRequestException("User not found.");
            }
            return new OperationResult<UserDto>() { Data = user };
        }

        public async Task<OperationResult<IEnumerable<UserDto>>> GetAllUsersAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var users = await _httpClient.GetFromJsonAsync<IEnumerable<UserDto>>("api/account/get-users");
            return new OperationResult<IEnumerable<UserDto>>() { Data = users ?? Enumerable.Empty<UserDto>() };
        }

        public async Task<OperationResult<string>> UpdateUserAsync(UserDto user, string token)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/account/update/{user.Email}", user);
                response.EnsureSuccessStatusCode();
                return new OperationResult<string> { Data = response.StatusCode == HttpStatusCode.NoContent ? "Success" : "Failed" };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<string> { Errors = [ex.InnerException == null ? ex.Message : ex.InnerException.Message] };
            }
        }

        public async Task<OperationResult<string>> DeleteUserAsync(UserDto user, string token)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/account/delete/{user.Email}");
                response.EnsureSuccessStatusCode();
                return new OperationResult<string> { Data = response.StatusCode == HttpStatusCode.NoContent ? "Success" : "Failed" };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<string> { Errors = [ex.InnerException == null ? ex.Message : ex.InnerException.Message] };
            }
        }

        public async Task<OperationResult<string>> RegisterUserAsync(RegisterUserDto registerDto, string token)
        {

            var response = await _httpClient.PostAsJsonAsync("api/account/register", registerDto);
            try
            {
                response.EnsureSuccessStatusCode();
                return new OperationResult<string> { Data = response.StatusCode == HttpStatusCode.OK ? "Success" : "Failed" };
            }
            catch
            {
                var content = await response.Content.ReadAsStringAsync();
                return new OperationResult<string> { Succeeded = false, Errors = content.Split("|") };
            }
        }

        public async Task<OperationResult<string>> LoginUserAsync(LoginDto loginDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Account/login", loginDto);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                return new OperationResult<string> { Data = data!.Token };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = [ex.InnerException == null ? ex.Message : ex.InnerException.Message] };
            }

        }
    }
}