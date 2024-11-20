using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;
using System.Net.Http.Json;

namespace AJKAccessGuard.Services
{
    public class PersonsApiService : IPersonsApiService
    {
        private readonly HttpClient _httpClient;

        public PersonsApiService(HttpClient httpClient)
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

        public async Task<OperationResult<PersonDTO>> GetPersonAsync(int id, string token)
        {
            SetAuthorizationHeader(token);
            try
            {
                var person = await _httpClient.GetFromJsonAsync<PersonDTO>($"api/persons/{id}").ConfigureAwait(false);
                if (person == null)
                {
                    throw new HttpRequestException("Person not found.");
                }
                return new OperationResult<PersonDTO> { Succeeded = true, Data = person };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<PersonDTO> { Succeeded = false, Errors = [ex.InnerException?.Message ?? ex.Message] };
            }
            catch (TaskCanceledException ex)
            {
                return new OperationResult<PersonDTO> { Succeeded = false, Errors = ["Request timed out.", ex.Message] };
            }
            catch (Exception ex)
            {
                return new OperationResult<PersonDTO> { Succeeded = false, Errors = [ex.Message] };
            }
        }

        public async Task<OperationResult<IEnumerable<PersonDTO>>> GetAllPersonsAsync(string token)
        {
            SetAuthorizationHeader(token);
            try
            {
                var persons = await _httpClient.GetFromJsonAsync<IEnumerable<PersonDTO>>("api/persons").ConfigureAwait(false);
                return new OperationResult<IEnumerable<PersonDTO>> { Succeeded = true, Data = persons ?? Enumerable.Empty<PersonDTO>() };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<IEnumerable<PersonDTO>> { Succeeded = false, Errors = [ex.InnerException?.Message ?? ex.Message] };
            }
            catch (TaskCanceledException ex)
            {
                return new OperationResult<IEnumerable<PersonDTO>> { Succeeded = false, Errors = ["Request timed out.", ex.Message] };
            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<PersonDTO>> { Succeeded = false, Errors = [ex.Message] };
            }
        }

        public async Task<OperationResult<string>> AddPersonAsync(PersonDTO personDTO, string token)
        {
            SetAuthorizationHeader(token);
            var response = new HttpResponseMessage();
            try
            {
                response = await _httpClient.PostAsJsonAsync("api/persons", personDTO).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return new OperationResult<string> { Succeeded = true, Data = await response.Content.ReadAsStringAsync() };
            }
            catch (HttpRequestException ex)
            {
                var responseMessages = (await response.Content.ReadAsStringAsync()).Split('|');
                return new OperationResult<string> { Succeeded = false, Errors = responseMessages.Length > 0 ? responseMessages : [ex.Message] };
            }
            catch (TaskCanceledException ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = ["Request timed out.", ex.Message] };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = [ex.Message] };
            }
        }

        public async Task<OperationResult<string>> UpdatePersonAsync(int id, PersonDTO personDTO, string token)
        {
            SetAuthorizationHeader(token);
            var response = new HttpResponseMessage();
            try
            {
                response = await _httpClient.PutAsJsonAsync($"api/persons/{id}", personDTO).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return new OperationResult<string> { Succeeded = true, Data = await response.Content.ReadAsStringAsync() };
            }
            catch (HttpRequestException ex)
            {
                var responseMessages = (await response.Content.ReadAsStringAsync()).Split('|');
                return new OperationResult<string> { Succeeded = false, Errors = responseMessages.Length > 0 ? responseMessages : [ex.Message] };
            }
            catch (TaskCanceledException ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new[] { "Request timed out.", ex.Message } };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new[] { ex.Message } };
            }
        }

        public async Task<OperationResult<string>> DeletePersonAsync(int id, string token)
        {
            SetAuthorizationHeader(token);
            var response = new HttpResponseMessage();
            try
            {
                response = await _httpClient.DeleteAsync($"api/persons/{id}").ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return new OperationResult<string> { Succeeded = true, Data = await response.Content.ReadAsStringAsync() };
            }
            catch (HttpRequestException ex)
            {
                var responseMessages = (await response.Content.ReadAsStringAsync()).Split('|');
                return new OperationResult<string> { Succeeded = false, Errors = responseMessages.Length > 0 ? responseMessages : [ex.Message] };
            }
            catch (TaskCanceledException ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = ["Request timed out.", ex.Message] };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = [ex.Message] };
            }
        }
    }
}
