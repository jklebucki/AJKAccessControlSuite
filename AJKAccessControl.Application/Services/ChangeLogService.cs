
using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Infrastructure.Repositories;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessControl.Application.Services
{
    public class ChangeLogService : IChangeLogService
    {
        private readonly IChangeLogRepository _changeLogRepository;

        public ChangeLogService(IChangeLogRepository changeLogRepository)
        {
            _changeLogRepository = changeLogRepository;
        }

        public async Task<OperationResult<ChangeLogDto>> GetByIdAsync(int id)
        {
            var changeLog = await _changeLogRepository.GetByIdAsync(id);
            if (changeLog == null)
            {
                return new OperationResult<ChangeLogDto> { Succeeded = false, Errors = new List<string> { "ChangeLog not found" } };
            }
            return new OperationResult<ChangeLogDto> { Succeeded = true, Data = MapToDTO(changeLog) };
        }

        public async Task<OperationResult<IEnumerable<ChangeLogDto>>> GetAllAsync()
        {
            var changeLogs = await _changeLogRepository.GetAllAsync();
            return new OperationResult<IEnumerable<ChangeLogDto>> { Succeeded = true, Data = changeLogs.Select(MapToDTO) };
        }

        public async Task<OperationResult<string>> AddAsync(ChangeLogDto changeLogDto)
        {
            try
            {
                var changeLog = MapToEntity(changeLogDto);
                var changeLogId = await _changeLogRepository.AddAsync(changeLog);
                return new OperationResult<string> { Succeeded = true, Data = $"{changeLogId}" };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<OperationResult<string>> UpdateAsync(ChangeLogDto changeLogDto)
        {
            var changeLog = await _changeLogRepository.GetByIdAsync(changeLogDto.Id);
            if (changeLog == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "ChangeLog not found" } };
            }

            try
            {
                changeLog.EntityName = changeLogDto.EntityName;
                changeLog.EntityId = changeLogDto.EntityId;
                changeLog.PropertyName = changeLogDto.PropertyName;
                changeLog.OldValue = changeLogDto.OldValue;
                changeLog.NewValue = changeLogDto.NewValue;
                changeLog.ChangedBy = changeLogDto.ChangedBy;
                changeLog.ChangedAt = changeLogDto.ChangedAt;
                var changeLogId = await _changeLogRepository.UpdateAsync(changeLog);
                return new OperationResult<string> { Succeeded = true, Data = $"{changeLogId}" };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<OperationResult<string>> DeleteAsync(int id)
        {
            var changeLog = await _changeLogRepository.GetByIdAsync(id);
            if (changeLog == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "ChangeLog not found" } };
            }

            await _changeLogRepository.DeleteAsync(id);
            return new OperationResult<string> { Succeeded = true, Data = "ChangeLog deleted successfully" };
        }

        private ChangeLogDto MapToDTO(ChangeLog changeLog)
        {
            return new ChangeLogDto
            {
                Id = changeLog.Id,
                EntityName = changeLog.EntityName,
                EntityId = changeLog.EntityId,
                PropertyName = changeLog.PropertyName,
                OldValue = changeLog.OldValue,
                NewValue = changeLog.NewValue,
                ChangedBy = changeLog.ChangedBy,
                ChangedAt = changeLog.ChangedAt
            };
        }

        private ChangeLog MapToEntity(ChangeLogDto changeLogDto)
        {
            return new ChangeLog
            {
                Id = changeLogDto.Id,
                EntityName = changeLogDto.EntityName,
                EntityId = changeLogDto.EntityId,
                PropertyName = changeLogDto.PropertyName,
                OldValue = changeLogDto.OldValue,
                NewValue = changeLogDto.NewValue,
                ChangedBy = changeLogDto.ChangedBy,
                ChangedAt = changeLogDto.ChangedAt
            };
        }
    }
}