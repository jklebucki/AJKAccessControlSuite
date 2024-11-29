
using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessControl.Application.Services
{
    public interface IChangeLogService
    {
        Task<OperationResult<ChangeLogDto>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<ChangeLogDto>>> GetAllAsync();
        Task<OperationResult<string>> AddAsync(ChangeLogDto changeLogDto);
        Task<OperationResult<string>> UpdateAsync(ChangeLogDto changeLogDto);
        Task<OperationResult<string>> DeleteAsync(int id);
    }
}