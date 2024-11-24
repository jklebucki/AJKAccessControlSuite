using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessGuard.Services
{
    public interface IVehiclesApiService
    {
        Task<OperationResult<VehicleDTO>> GetVehicleAsync(int id, string token);
        Task<OperationResult<IEnumerable<VehicleDTO>>> GetAllVehiclesAsync(string token);
        Task<OperationResult<string>> AddVehicleAsync(VehicleDTO vehicleDTO, string token);
        Task<OperationResult<string>> UpdateVehicleAsync(int id, VehicleDTO vehicleDTO, string token);
        Task<OperationResult<string>> DeleteVehicleAsync(int id, string token);
    }
}
