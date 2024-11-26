using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;
using System.Net.Http.Json;

namespace AJKAccessGuard.Services
{
    public class VehiclesApiService : IVehiclesApiService
    {
        private readonly HttpClient _httpClient;

        public VehiclesApiService(HttpClient httpClient)
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

        public async Task<OperationResult<VehicleDto>> GetVehicleAsync(int id, string token)
        {
            SetAuthorizationHeader(token);
            try
            {
                var vehicle = await _httpClient.GetFromJsonAsync<VehicleDto>($"api/vehicles/{id}").ConfigureAwait(false);
                if (vehicle == null)
                {
                    throw new HttpRequestException("Vehicle not found.");
                }
                return new OperationResult<VehicleDto> { Succeeded = true, Data = vehicle };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<VehicleDto> { Succeeded = false, Errors = [ex.InnerException?.Message ?? ex.Message] };
            }
            catch (TaskCanceledException ex)
            {
                return new OperationResult<VehicleDto> { Succeeded = false, Errors = ["Request timed out.", ex.Message] };
            }
            catch (Exception ex)
            {
                return new OperationResult<VehicleDto> { Succeeded = false, Errors = [ex.Message] };
            }
        }

        public async Task<OperationResult<IEnumerable<VehicleDto>>> GetAllVehiclesAsync(string token)
        {
            SetAuthorizationHeader(token);
            try
            {
                var vehicles = await _httpClient.GetFromJsonAsync<IEnumerable<VehicleDto>>("api/vehicles").ConfigureAwait(false);
                return new OperationResult<IEnumerable<VehicleDto>> { Succeeded = true, Data = vehicles ?? Enumerable.Empty<VehicleDto>() };
            }
            catch (HttpRequestException ex)
            {
                return new OperationResult<IEnumerable<VehicleDto>> { Succeeded = false, Errors = [ex.InnerException?.Message ?? ex.Message] };
            }
            catch (TaskCanceledException ex)
            {
                return new OperationResult<IEnumerable<VehicleDto>> { Succeeded = false, Errors = ["Request timed out.", ex.Message] };
            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<VehicleDto>> { Succeeded = false, Errors = [ex.Message] };
            }
        }

        public async Task<OperationResult<string>> AddVehicleAsync(VehicleDto vehicleDTO, string token)
        {
            SetAuthorizationHeader(token);
            var response = new HttpResponseMessage();
            try
            {
                response = await _httpClient.PostAsJsonAsync("api/vehicles", vehicleDTO).ConfigureAwait(false);
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

        public async Task<OperationResult<string>> UpdateVehicleAsync(int id, VehicleDto vehicleDTO, string token)
        {
            SetAuthorizationHeader(token);
            var response = new HttpResponseMessage();
            try
            {
                response = await _httpClient.PutAsJsonAsync($"api/vehicles/{id}", vehicleDTO).ConfigureAwait(false);
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

        public async Task<OperationResult<string>> DeleteVehicleAsync(int id, string token)
        {
            SetAuthorizationHeader(token);
            var response = new HttpResponseMessage();
            try
            {
                response = await _httpClient.DeleteAsync($"api/vehicles/{id}").ConfigureAwait(false);
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
