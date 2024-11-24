
using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessControl.Application.Services
{
    public interface IVehicleService
    {
        Task<OperationResult<VehicleDTO>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<VehicleDTO>>> GetAllAsync();
        Task<OperationResult<string>> AddAsync(VehicleDTO vehicleDTO);
        Task<OperationResult<string>> UpdateAsync(VehicleDTO vehicleDTO);
        Task<OperationResult<string>> DeleteAsync(int id);
    }
}