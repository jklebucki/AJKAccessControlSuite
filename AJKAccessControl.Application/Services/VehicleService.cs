
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

        public async Task<OperationResult<VehicleDto>> GetByIdAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                return new OperationResult<VehicleDto> { Succeeded = false, Errors = new List<string> { "Vehicle not found" } };
            }
            return new OperationResult<VehicleDto> { Succeeded = true, Data = MapToDTO(vehicle) };
        }

        public async Task<OperationResult<IEnumerable<VehicleDto>>> GetAllAsync()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            return new OperationResult<IEnumerable<VehicleDto>> { Succeeded = true, Data = vehicles.Select(MapToDTO) };
        }

        public async Task<OperationResult<string>> AddAsync(VehicleDto vehicleDto)
        {
            try
            {
                var vehicle = MapToEntity(vehicleDto);
                var vehicleId = await _vehicleRepository.AddAsync(vehicle);
                return new OperationResult<string> { Succeeded = true, Data = $"{vehicleId}" };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<OperationResult<string>> UpdateAsync(VehicleDto vehicleDto)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleDto.Id);
            if (vehicle == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "Vehicle not found" } };
            }

            try
            {
                vehicle.PlateNumber = vehicleDto.PlateNumber;
                vehicle.IsCompanyCar = vehicleDto.IsCompanyCar;
                vehicle.Company = vehicleDto.Company;
                vehicle.Owner = vehicleDto.Owner;
                vehicle.CreatedBy = vehicleDto.CreatedBy;
                vehicle.CreatedAt = vehicleDto.CreatedAt;
                vehicle.UpdatedAt = vehicleDto.UpdatedAt;
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

        private VehicleDto MapToDTO(Vehicle vehicle)
        {
            return new VehicleDto
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

        private Vehicle MapToEntity(VehicleDto vehicleDto)
        {
            return new Vehicle
            {
                Id = vehicleDto.Id,
                PlateNumber = vehicleDto.PlateNumber,
                IsCompanyCar = vehicleDto.IsCompanyCar,
                Company = vehicleDto.Company,
                Owner = vehicleDto.Owner,
                CreatedBy = vehicleDto.CreatedBy,
                CreatedAt = vehicleDto.CreatedAt,
                UpdatedAt = vehicleDto.UpdatedAt
            };
        }
    }
}