
using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessGuard.Services
{
    public interface IAccessEntriesApiService
    {
        Task<OperationResult<AccessEntryDto>> GetAccessEntryAsync(int id, string token);
        Task<OperationResult<IEnumerable<AccessEntryDto>>> GetAllAccessEntriesAsync(string token);
        Task<OperationResult<string>> AddAccessEntryAsync(AccessEntryDto accessEntryDTO, string token);
        Task<OperationResult<string>> UpdateAccessEntryAsync(int id, AccessEntryDto accessEntryDTO, string token);
        Task<OperationResult<string>> DeleteAccessEntryAsync(int id, string token);
    }
}