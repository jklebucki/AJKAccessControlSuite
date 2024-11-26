
using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessControl.Application.Services
{
    public interface IVehicleService
    {
        Task<OperationResult<VehicleDto>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<VehicleDto>>> GetAllAsync();
        Task<OperationResult<string>> AddAsync(VehicleDto vehicleDTO);
        Task<OperationResult<string>> UpdateAsync(VehicleDto vehicleDTO);
        Task<OperationResult<string>> DeleteAsync(int id);
    }
}