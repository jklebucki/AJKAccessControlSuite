
using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;
using System.Net.Http.Json;

namespace AJKAccessGuard.Services
{
    public class AccessEntriesApiService : IAccessEntriesApiService
    {
        private readonly HttpClient _httpClient;

        public AccessEntriesApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void SetAuthorizationHeader(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("Token cannot be null or whitespace.", nameof(token));
            }

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public async Task<OperationResult<AccessEntryDto>> GetAccessEntryAsync(int id, string token)
        {
            SetAuthorizationHeader(token);
            try
            {
                var accessEntry = await _httpClient.GetFromJsonAsync<AccessEntryDto>($"api/accessentry/{id}").ConfigureAwait(false);
                if (accessEntry == null)
                {
                    throw new HttpRequestException("Access entry not found.");
                }
                return new OperationResult<AccessEntryDto> { Succeeded = true, Data = accessEntry };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<AccessEntryDto> { Succeeded = false, Errors = new List<string> { ex.InnerException?.Message ?? ex.Message } };
            }
            catch (TaskCanceledException ex)
            {
                return new OperationResult<AccessEntryDto> { Succeeded = false, Errors = new List<string> { "Request timed out.", ex.Message } };
            }
            catch (Exception ex)
            {
                return new OperationResult<AccessEntryDto> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<OperationResult<IEnumerable<AccessEntryDto>>> GetAllAccessEntriesAsync(string token)
        {
            SetAuthorizationHeader(token);
            try
            {
                var accessEntries = await _httpClient.GetFromJsonAsync<IEnumerable<AccessEntryDto>>("api/accessentry").ConfigureAwait(false);
                return new OperationResult<IEnumerable<AccessEntryDto>> { Succeeded = true, Data = accessEntries ?? Enumerable.Empty<AccessEntryDto>() };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<IEnumerable<AccessEntryDto>> { Succeeded = false, Errors = new List<string> { ex.InnerException?.Message ?? ex.Message } };
            }
            catch (TaskCanceledException ex)
            {
                return new OperationResult<IEnumerable<AccessEntryDto>> { Succeeded = false, Errors = new List<string> { "Request timed out.", ex.Message } };
            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<AccessEntryDto>> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<OperationResult<string>> AddAccessEntryAsync(AccessEntryDto accessEntryDTO, string token)
        {
            SetAuthorizationHeader(token);
            var response = new HttpResponseMessage();
            try
            {
                response = await _httpClient.PostAsJsonAsync("api/accessentry", accessEntryDTO).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return new OperationResult<string> { Succeeded = true, Data = await response.Content.ReadAsStringAsync() };
            }
            catch (HttpRequestException ex)
            {
                var responseMessages = (await response.Content.ReadAsStringAsync()).Split('|');
                return new OperationResult<string> { Succeeded = false, Errors = responseMessages.Length > 0 ? responseMessages.ToList() : new List<string> { ex.Message } };
            }
            catch (TaskCanceledException ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "Request timed out.", ex.Message } };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<OperationResult<string>> UpdateAccessEntryAsync(int id, AccessEntryDto accessEntryDTO, string token)
        {
            SetAuthorizationHeader(token);
            var response = new HttpResponseMessage();
            try
            {
                response = await _httpClient.PutAsJsonAsync($"api/accessentry/{id}", accessEntryDTO).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return new OperationResult<string> { Succeeded = true, Data = await response.Content.ReadAsStringAsync() };
            }
            catch (HttpRequestException ex)
            {
                var responseMessages = (await response.Content.ReadAsStringAsync()).Split('|');
                return new OperationResult<string> { Succeeded = false, Errors = responseMessages.Length > 0 ? responseMessages.ToList() : new List<string> { ex.Message } };
            }
            catch (TaskCanceledException ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "Request timed out.", ex.Message } };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<OperationResult<string>> DeleteAccessEntryAsync(int id, string token)
        {
            SetAuthorizationHeader(token);
            var response = new HttpResponseMessage();
            try
            {
                response = await _httpClient.DeleteAsync($"api/accessentry/{id}").ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return new OperationResult<string> { Succeeded = true, Data = await response.Content.ReadAsStringAsync() };
            }
            catch (HttpRequestException ex)
            {
                var responseMessages = (await response.Content.ReadAsStringAsync()).Split('|');
                return new OperationResult<string> { Succeeded = false, Errors = responseMessages.Length > 0 ? responseMessages.ToList() : new List<string> { ex.Message } };
            }
            catch (TaskCanceledException ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "Request timed out.", ex.Message } };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }
    }
}