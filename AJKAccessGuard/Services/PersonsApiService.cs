using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;
using System.Net;
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

        public async Task<OperationResult<PersonDTO>> GetPersonAsync(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                var person = await _httpClient.GetFromJsonAsync<PersonDTO>($"api/persons/{id}");
                if (person == null)
                {
                    throw new HttpRequestException("Person not found.");
                }
                return new OperationResult<PersonDTO> { Succeeded = true, Data = person };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<PersonDTO> { Succeeded = false, Errors = new[] { ex.InnerException == null ? ex.Message : ex.InnerException.Message } };
            }
        }

        public async Task<OperationResult<IEnumerable<PersonDTO>>> GetAllPersonsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                var persons = await _httpClient.GetFromJsonAsync<IEnumerable<PersonDTO>>("api/persons");
                return new OperationResult<IEnumerable<PersonDTO>> { Succeeded = true, Data = persons ?? Enumerable.Empty<PersonDTO>() };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<IEnumerable<PersonDTO>> { Succeeded = false, Errors = new[] { ex.InnerException == null ? ex.Message : ex.InnerException.Message } };
            }
        }

        public async Task<OperationResult<string>> AddPersonAsync(PersonDTO personDTO, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/persons", personDTO);
                response.EnsureSuccessStatusCode();
                return new OperationResult<string> { Data = response.StatusCode == HttpStatusCode.Created ? "Success" : "Failed" };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new[] { ex.InnerException == null ? ex.Message : ex.InnerException.Message } };
            }
        }

        public async Task<OperationResult<string>> UpdatePersonAsync(int id, PersonDTO personDTO, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/persons/{id}", personDTO);
                response.EnsureSuccessStatusCode();
                return new OperationResult<string> { Data = response.StatusCode == HttpStatusCode.NoContent ? "Success" : "Failed" };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new[] { ex.InnerException == null ? ex.Message : ex.InnerException.Message } };
            }
        }

        public async Task<OperationResult<string>> DeletePersonAsync(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                var response = await _httpClient.DeleteAsync($"api/persons/{id}");
                response.EnsureSuccessStatusCode();
                return new OperationResult<string> { Data = response.StatusCode == HttpStatusCode.NoContent ? "Success" : "Failed" };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new[] { ex.InnerException == null ? ex.Message : ex.InnerException.Message } };
            }
        }
    }
}
