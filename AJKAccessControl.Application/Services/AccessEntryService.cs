
using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Infrastructure.Repositories;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessControl.Application.Services
{
    public class AccessEntryService : IAccessEntryService
    {
        private readonly IAccessEntryRepository _accessEntryRepository;

        public AccessEntryService(IAccessEntryRepository accessEntryRepository)
        {
            _accessEntryRepository = accessEntryRepository;
        }

        public async Task<OperationResult<AccessEntryDto>> GetByIdAsync(int id)
        {
            var accessEntry = await _accessEntryRepository.GetByIdAsync(id);
            if (accessEntry == null)
            {
                return new OperationResult<AccessEntryDto> { Succeeded = false, Errors = new List<string> { "AccessEntry not found" } };
            }
            return new OperationResult<AccessEntryDto> { Succeeded = true, Data = MapToDTO(accessEntry) };
        }

        public async Task<OperationResult<IEnumerable<AccessEntryDto>>> GetAllAsync()
        {
            var accessEntries = await _accessEntryRepository.GetAllAsync();
            return new OperationResult<IEnumerable<AccessEntryDto>> { Succeeded = true, Data = accessEntries.Select(MapToDTO) };
        }

        public async Task<OperationResult<string>> AddAsync(AccessEntryDto accessEntryDto)
        {
            try
            {
                var accessEntry = MapToEntity(accessEntryDto);
                var accessEntryId = await _accessEntryRepository.AddAsync(accessEntry);
                return new OperationResult<string> { Succeeded = true, Data = $"{accessEntryId}" };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<OperationResult<string>> UpdateAsync(AccessEntryDto accessEntryDto)
        {
            var accessEntry = await _accessEntryRepository.GetByIdAsync(accessEntryDto.Id);
            if (accessEntry == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "AccessEntry not found" } };
            }

            try
            {
                accessEntry.EntityId = accessEntryDto.EntityId;
                accessEntry.EntityType = accessEntryDto.EntityType;
                accessEntry.EntryTime = accessEntryDto.EntryTime;
                accessEntry.ExitTime = accessEntryDto.ExitTime;
                accessEntry.Description = accessEntryDto.Description;
                accessEntry.CreatedBy = accessEntryDto.CreatedBy;
                accessEntry.CreatedAt = accessEntryDto.CreatedAt;
                accessEntry.UpdatedAt = accessEntryDto.UpdatedAt;
                var accessEntryId = await _accessEntryRepository.UpdateAsync(accessEntry);
                return new OperationResult<string> { Succeeded = true, Data = $"{accessEntryId}" };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<OperationResult<string>> DeleteAsync(int id)
        {
            var accessEntry = await _accessEntryRepository.GetByIdAsync(id);
            if (accessEntry == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "AccessEntry not found" } };
            }

            await _accessEntryRepository.DeleteAsync(id);
            return new OperationResult<string> { Succeeded = true, Data = "AccessEntry deleted successfully" };
        }

        private AccessEntryDto MapToDTO(AccessEntry accessEntry)
        {
            return new AccessEntryDto
            {
                Id = accessEntry.Id,
                EntityId = accessEntry.EntityId,
                EntityType = accessEntry.EntityType,
                EntryTime = accessEntry.EntryTime,
                ExitTime = accessEntry.ExitTime,
                Description = accessEntry.Description,
                CreatedBy = accessEntry.CreatedBy,
                UpdatedAt = accessEntry.UpdatedAt
            };
        }

        private AccessEntry MapToEntity(AccessEntryDto accessEntryDto)
        {
            return new AccessEntry
            {
                Id = accessEntryDto.Id,
                EntityId = accessEntryDto.EntityId,
                EntityType = accessEntryDto.EntityType,
                EntryTime = accessEntryDto.EntryTime,
                ExitTime = accessEntryDto.ExitTime,
                Description = accessEntryDto.Description,
                CreatedBy = accessEntryDto.CreatedBy,
                CreatedAt = accessEntryDto.CreatedAt,
                UpdatedAt = accessEntryDto.UpdatedAt
            };
        }
    }
}