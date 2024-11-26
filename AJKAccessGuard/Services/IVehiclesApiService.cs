using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessGuard.Services
{
    public interface IVehiclesApiService
    {
        Task<OperationResult<VehicleDto>> GetVehicleAsync(int id, string token);
        Task<OperationResult<IEnumerable<VehicleDto>>> GetAllVehiclesAsync(string token);
        Task<OperationResult<string>> AddVehicleAsync(VehicleDto vehicleDTO, string token);
        Task<OperationResult<string>> UpdateVehicleAsync(int id, VehicleDto vehicleDTO, string token);
        Task<OperationResult<string>> DeleteVehicleAsync(int id, string token);
    }
}
