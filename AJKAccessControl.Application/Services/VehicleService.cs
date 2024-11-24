
using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Infrastructure.Repositories;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessControl.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<OperationResult<VehicleDTO>> GetByIdAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                return new OperationResult<VehicleDTO> { Succeeded = false, Errors = new List<string> { "Vehicle not found" } };
            }
            return new OperationResult<VehicleDTO> { Succeeded = true, Data = MapToDTO(vehicle) };
        }

        public async Task<OperationResult<IEnumerable<VehicleDTO>>> GetAllAsync()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            return new OperationResult<IEnumerable<VehicleDTO>> { Succeeded = true, Data = vehicles.Select(MapToDTO) };
        }

        public async Task<OperationResult<string>> AddAsync(VehicleDTO vehicleDTO)
        {
            try
            {
                var vehicle = MapToEntity(vehicleDTO);
                var vehicleId = await _vehicleRepository.AddAsync(vehicle);
                return new OperationResult<string> { Succeeded = true, Data = $"{vehicleId}" };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<OperationResult<string>> UpdateAsync(VehicleDTO vehicleDTO)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleDTO.Id);
            if (vehicle == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "Vehicle not found" } };
            }

            try
            {
                vehicle.PlateNumber = vehicleDTO.PlateNumber;
                vehicle.IsCompanyCar = vehicleDTO.IsCompanyCar;
                vehicle.Company = vehicleDTO.Company;
                vehicle.Owner = vehicleDTO.Owner;
                vehicle.CreatedBy = vehicleDTO.CreatedBy;
                vehicle.CreatedAt = vehicleDTO.CreatedAt;
                vehicle.UpdatedAt = vehicleDTO.UpdatedAt;
                var vehicleId = await _vehicleRepository.UpdateAsync(vehicle);
                return new OperationResult<string> { Succeeded = true, Data = $"{vehicleId}" };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<OperationResult<string>> DeleteAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "Vehicle not found" } };
            }

            await _vehicleRepository.DeleteAsync(id);
            return new OperationResult<string> { Succeeded = true, Data = "Vehicle deleted successfully" };
        }

        private VehicleDTO MapToDTO(Vehicle vehicle)
        {
            return new VehicleDTO
            {
                Id = vehicle.Id,
                PlateNumber = vehicle.PlateNumber,
                IsCompanyCar = vehicle.IsCompanyCar,
                Company = vehicle.Company,
                Owner = vehicle.Owner,
                CreatedBy = vehicle.CreatedBy,
                CreatedAt = vehicle.CreatedAt,
                UpdatedAt = vehicle.UpdatedAt
            };
        }

        private Vehicle MapToEntity(VehicleDTO vehicleDTO)
        {
            return new Vehicle
            {
                Id = vehicleDTO.Id,
                PlateNumber = vehicleDTO.PlateNumber,
                IsCompanyCar = vehicleDTO.IsCompanyCar,
                Company = vehicleDTO.Company,
                Owner = vehicleDTO.Owner,
                CreatedBy = vehicleDTO.CreatedBy,
                CreatedAt = vehicleDTO.CreatedAt,
                UpdatedAt = vehicleDTO.UpdatedAt
            };
        }
    }
}