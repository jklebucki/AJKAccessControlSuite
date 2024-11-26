
using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessControl.Application.Services
{
    public interface IAccessEntryService
    {
        Task<OperationResult<AccessEntryDto>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<AccessEntryDto>>> GetAllAsync();
        Task<OperationResult<string>> AddAsync(AccessEntryDto accessEntryDto);
        Task<OperationResult<string>> UpdateAsync(AccessEntryDto accessEntryDto);
        Task<OperationResult<string>> DeleteAsync(int id);
    }
}